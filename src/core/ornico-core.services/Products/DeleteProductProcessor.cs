using System;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Products;
using ornico.common.infrastructure.UnitOfWorks;
using ornico.core.contracts.Products;
using ornico.core.repository.ContractRepositories;

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

        public Task<ProductForDeletionUiModel> SoftDeleteProductAsync(Guid userAuditId, Guid id)
        {
          throw new NotImplementedException();
        }

        public Task<ProductForDeletionUiModel> HardDeleteProductAsync(Guid userAuditId, Guid id)
        {
          throw new NotImplementedException();
        }

        public Task<ProductForDeletionUiModel> DeleteProductAsync(Guid productToBeDeletedId)
        {
          throw new NotImplementedException();
        }
    }
}