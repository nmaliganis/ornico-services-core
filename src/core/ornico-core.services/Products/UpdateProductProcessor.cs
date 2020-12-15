using System;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Products;
using ornico.common.infrastructure.TypeMappings;
using ornico.common.infrastructure.UnitOfWorks;
using ornico.core.contracts.Products;
using ornico.core.repository.ContractRepositories;

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

        public Task<ProductUiModel> UpdateProductAsync(ProductForModificationUiModel updatedProduct)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDeviceProvisioningUiModel> ProvisioningDeviceToProductAsync(Guid userAuditId, Guid id, Guid deviceId,
          ProductForModificationProvisioningModel productForModificationProvisioningModel)
        {
          throw new NotImplementedException();
        }
    }
}