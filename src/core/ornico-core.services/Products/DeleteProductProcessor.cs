using System;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Products;
using ornico.common.infrastructure.Exceptions.Domain.Products;
using ornico.common.infrastructure.UnitOfWorks;
using ornico.core.contracts.Products;
using ornico.core.model.Products;
using ornico.core.repository.ContractRepositories;
using Serilog;

namespace ornico.core.services.Products
{
  public class DeleteProductProcessor : IDeleteProductProcessor
  {
    private readonly IUnitOfWork _uOf;
    private readonly IProductRepository _productRepository;

    public DeleteProductProcessor(IUnitOfWork uOf,
      IProductRepository productRepository)
    {
      _uOf = uOf;
      _productRepository = productRepository;
    }

    public Task<ProductForDeletionUiModel> DeleteProductAsync(Guid productToBeDeletedId)
    {
      var response =
        new ProductForDeletionUiModel()
        {
          Message = "START_HARD_DELETION",
          DeletionStatus = false
        };

      if (productToBeDeletedId == Guid.Empty)
      {
        response.Message = "ERROR_INVALID_PRODUCT_ID";
        response.DeletionStatus = false;
        return Task.Run(() => response);
      }

      try
      {
        var productToBeSoftDeleted = _productRepository.FindBy(productToBeDeletedId);

        if (productToBeSoftDeleted == null)
          throw new ProductDoesNotExistException(productToBeDeletedId);

        Log.Debug(
          $"Update-Delete Product: with Id: {productToBeDeletedId}" +
          "--HardDeleteProduct--  @Ready@ [DeleteProductProcessor]. " +
          "Message: Just Before MakeProductTransient");

        MakeProductTransient(productToBeSoftDeleted);

        Log.Debug(
          $"Update-Delete Product: with Id: {productToBeDeletedId}" +
          "--HardDeleteProduct--  @Ready@ [DeleteProductProcessor]. " +
          "Message: Just After MakeProductTransient");

        response.DeletionStatus = ThrowExcIfProductWasNotBeMadeTransient(productToBeSoftDeleted);
        response.Message = "SUCCESS_DELETION";
      }
      catch (ProductDoesNotExistException e)
      {
        response.Message = "ERROR_PRODUCT_DOES_NOT_EXIST";
        Log.Error(
          $"Delete Product: Id: {productToBeDeletedId}" +
          $"Error Message:{response.Message}" +
          "--HardDeleteProduct--  @NotComplete@ [DeleteProductProcessor]. " +
          $"@innerfault:{e?.Message} and {e?.InnerException}");
      }
      catch (ProductDoesExistAfterMadeTransientException ex)
      {
        response.Message = "ERROR_PRODUCT_DOES_NOT_MADE_TRANSIENT";
        Log.Error(
          $"Delete Product: Id: {productToBeDeletedId}" +
          $"Error Message:{response.Message}" +
          "--HardDeleteProduct--  @NotComplete@ [DeleteProductProcessor]. " +
          $"@innerfault:{ex?.Message} and {ex?.InnerException}");
      }
      catch (Exception exxx)
      {
        response.Message = "UNKNOWN_ERROR";
        Log.Error(
          $"Delete Product: Id: {productToBeDeletedId}" +
          $"Error Message:{response.Message}" +
          $"--HardDeleteProduct--  @fail@ [DeleteProductProcessor]. " +
          $"@innerfault:{exxx.Message} and {exxx.InnerException}");
      }

      return Task.Run(() => response);
    }

    private bool ThrowExcIfProductWasNotBeMadeTransient(Product productToBeSoftDeleted)
    {
      var productHaveBeenDeleted =
        _productRepository.FindBy(productToBeSoftDeleted.Id);
      return productHaveBeenDeleted != null
        ? throw new ProductDoesExistAfterMadeTransientException(productToBeSoftDeleted.Id)
        : true;
    }

    private void MakeProductTransient(Product productToBeSoftDeleted)
    {
      _productRepository.Remove(productToBeSoftDeleted);
      _uOf.Commit();
    }
  }
}