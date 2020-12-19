using System;
using System.Runtime.InteropServices.WindowsRuntime;
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
  public class UpdateProductProcessor : IUpdateProductProcessor
  {
    private readonly IUnitOfWork _uOf;
    private readonly IProductRepository _productRepository;
    private readonly IAutoMapper _autoMapper;

    public UpdateProductProcessor(IUnitOfWork uOf, IAutoMapper autoMapper, IProductRepository productRepository)
    {
      _uOf = uOf;
      _productRepository = productRepository;
      _autoMapper = autoMapper;
    }

    public Task<ProductUiModel> UpdateProductAsync(Guid productIdToBeModified, ProductForModificationUiModel updatedProduct)
    {
      var response =
        new ProductUiModel()
        {
          Message = "SUCCESS_MODIFICATION"
        };

      if (updatedProduct == null)
      {
        response.Message = "ERROR_INVALID_PRODUCT_MODEL";
        return Task.Run(() => response);
      }

      if (productIdToBeModified == Guid.Empty)
      {
        response.Message = "ERROR_INVALID_PRODUCT_ID";
        return Task.Run(() => response);
      }

      try
      {
        var productToBeModified = ThrowExceptionIfProductDoesNotExist(productIdToBeModified);

        productToBeModified.ModifyWith(updatedProduct.ProductName, 
          updatedProduct.ProductDescription, updatedProduct.ProductPrice);

        Log.Debug(
          $"Update-Modify Product: with Id: {productIdToBeModified}" +
          "--UpdateProduct--  @Ready@ [UpdateProductAsync]. " +
          "Message: Just Before MakeItPersistence");

        MakeProductPersistent(productToBeModified);

        Log.Debug(
          $"Update-Modify Product: with Id: {productIdToBeModified}" +
          "--UpdateProduct--  @Ready@ [UpdateProductAsync]. " +
          "Message: Just After MakeItPersistence");

        response = ThrowExcIfModificationWasNotBeMadePersistent(productIdToBeModified);
        response.Message = "SUCCESS_MODIFICATION";
      }
      catch (ProductDoesNotExistException e)
      {
        response.Message = "ERROR_PRODUCT_NOT_EXIST";
        Log.Error(
          $"Modify Product: {updatedProduct.ProductName}" +
          "--ModifyProductAsync-- @fail@ [UpdateProductProcessor]." +
          $" @innerfault:{e?.Message} and {e?.InnerException}");
      }
      catch (InvalidProductException e)
      {
        response.Message = "ERROR_INVALID_PRODUCT_MODEL";
        Log.Error(
          $"Modify Product: {updatedProduct.ProductName}" +
          "--ModifyProductAsync--  @fail@ [UpdateProductProcessor]." +
          $" @innerfault:{e?.Message} and {e?.InnerException}");
      }
      catch (ProductDoesNotExistAfterMadePersistentException e)
      {
        response.Message = "ERROR_MODIFICATION_NOT_MADE_PERSISTENT";
        Log.Error(
          $"Modify Product: {updatedProduct.ProductName}" +
          "--ModifyProductAsync--  @fail@ [UpdateProductProcessor]." +
          $" @innerfault:{e?.Message} and {e?.InnerException}");
      }
      catch (Exception e)
      {
        response.Message = "UNKNOWN_ERROR";
        Log.Error(
          $"Modify Product: {updatedProduct.ProductName}" +
          "--ModifyProductAsync--  @fail@ [UpdateProductProcessor]." +
          $" @innerfault:{e?.Message} and {e?.InnerException}");
      }


      return Task.Run(() => response);
    }

    private ProductUiModel ThrowExcIfModificationWasNotBeMadePersistent(Guid productIdToBeModified)
    {
      var productToHaveBeenModified  = _productRepository.FindBy(productIdToBeModified);

      if (productToHaveBeenModified == null)
        throw new ProductDoesNotExistAfterMadePersistentException(productIdToBeModified);

      return new ProductUiModel()
      {
        Message = "SUCCESS_MODIFICATION",
        Id = productToHaveBeenModified.Id,
        ProductName = productToHaveBeenModified.Name,
        ProductDescription = productToHaveBeenModified.Description,
        ProductPrice = productToHaveBeenModified.Price
      };
    }

    private Product ThrowExceptionIfProductDoesNotExist(Guid idProduct)
    {
      var productToBeRetrieved = _productRepository.FindBy(idProduct);
      if (productToBeRetrieved == null)
        throw new ProductDoesNotExistException(idProduct);
      return productToBeRetrieved;
    }

    private void MakeProductPersistent(Product productToBeMadePersistence)
    {
      _productRepository.Save(productToBeMadePersistence);
      _uOf.Commit();
    }
  }
}