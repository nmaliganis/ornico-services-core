using System;
using System.Linq;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Products;
using ornico.common.infrastructure.Exceptions.Domain.Products;
using ornico.common.infrastructure.TypeMappings;
using ornico.common.infrastructure.UnitOfWorks;
using ornico.core.contracts.Products;
using ornico.core.model.Products;
using ornico.core.repository.ContractRepositories;
using Serilog;

namespace ornico.core.services.Products
{
  public class CreateProductProcessor : ICreateProductProcessor
  {
    private readonly IUnitOfWork _uOf;
    private readonly IProductRepository _productRepository;
    private readonly IAutoMapper _autoMapper;

    public CreateProductProcessor(IUnitOfWork uOf, IAutoMapper autoMapper, IProductRepository productRepository)
    {
      _uOf = uOf;
      _productRepository = productRepository;
      _autoMapper = autoMapper;
    }

    public Task<ProductUiModel> CreateProductAsync(ProductForCreationUiModel newProductUiModel)
    {
      var response =
        new ProductUiModel()
        {
          Message = "START_CREATION"
        };

      if (newProductUiModel == null)
      {
        response.Message = "ERROR_INVALID_PRODUCT_MODEL";
        return Task.Run(() => response);
      }

      var productToBeCreated = new Product();

      try
      {
        productToBeCreated.InjectWithInitialAttributes(newProductUiModel.ProductName,
          newProductUiModel.ProductDescription, newProductUiModel.ProductPrice);

        ThrowExcIfProductCannotBeCreated(productToBeCreated);
        ThrowExcIfThisProductAlreadyExist(productToBeCreated);

        Log.Debug(
          $"Create Product: {newProductUiModel.ProductName}" +
          "--CreateProduct--  @NotComplete@ [CreateProductProcessor]. " +
          "Message: Just Before MakeItPersistence");

        MakeProductPersistent(productToBeCreated);

        Log.Debug(
          $"Create Product: {newProductUiModel.ProductName}" +
          "--CreateProduct--  @NotComplete@ [CreateProductProcessor]. " +
          "Message: Just After MakeItPersistence");
        response = ThrowExcIfProductWasNotBeMadePersistent(productToBeCreated);
        response.Message = "SUCCESS_CREATION";
      }
      catch (InvalidProductException e)
      {
        response.Message = "ERROR_INVALID_Product_MODEL";
        Log.Error(
          $"Create Product: {newProductUiModel.ProductName}" +
          "--CreateProduct--  @NotComplete@ [CreateProductProcessor]. " +
          $"Broken rules: {e.BrokenRules}");
      }
      catch (ProductAlreadyExistsException ex)
      {
        response.Message = "ERROR_Product_ALREADY_EXISTS";
        Log.Error(
          $"Create Product: {newProductUiModel.ProductName}" +
          "--CreateProduct--  @fail@ [CreateProductProcessor]. " +
          $"@innerfault:{ex?.Message} and {ex?.InnerException}");
      }
      catch (ProductDoesNotExistAfterMadePersistentException exx)
      {
        response.Message = "ERROR_Product_NOT_MADE_PERSISTENT";
        Log.Error(
          $"Create Product: {newProductUiModel.ProductName}" +
          "--CreateProduct--  @fail@ [CreateProductProcessor]." +
          $" @innerfault:{exx?.Message} and {exx?.InnerException}");
      }
      catch (Exception exxx)
      {
        response.Message = "UNKNOWN_ERROR";
        Log.Error(
          $"Create Product: {newProductUiModel.ProductName}" +
          $"--CreateProduct--  @fail@ [CreateProductProcessor]. " +
          $"@innerfault:{exxx.Message} and {exxx.InnerException}");
      }

      return Task.Run(() => response);
    }

    private void ThrowExcIfThisProductAlreadyExist(Product productToBeCreated)
    {
      var customerRetrieved = _productRepository.FindOneProductByMobile(productToBeCreated.Name);
      if (customerRetrieved != null)
      {
        throw new ProductAlreadyExistsException(productToBeCreated.Name,
          productToBeCreated.GetBrokenRulesAsString());
      }
    }
    private ProductUiModel ThrowExcIfProductWasNotBeMadePersistent(Product productToBeCreated)
    {
      var retrievedProduct = _productRepository.FindOneProductByMobile(productToBeCreated.Name);
      if (retrievedProduct != null)
        return _autoMapper.Map<ProductUiModel>(retrievedProduct);
      throw new ProductDoesNotExistAfterMadePersistentException(productToBeCreated.Name);
    }

    private void ThrowExcIfProductCannotBeCreated(Product productToBeCreated)
    {
      bool canBeCreated = !productToBeCreated.GetBrokenRules().Any();
      if (!canBeCreated)
        throw new InvalidProductException(productToBeCreated.GetBrokenRulesAsString());
    }

    private void MakeProductPersistent(Product productToBeMadePersistence)
    {
      _productRepository.Save(productToBeMadePersistence);
      _uOf.Commit();
    }
  }
}
