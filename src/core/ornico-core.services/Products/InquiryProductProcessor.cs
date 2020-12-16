using System;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Products;
using ornico.common.infrastructure.TypeMappings;
using ornico.core.contracts.Products;
using ornico.core.repository.ContractRepositories;

namespace ornico.core.services.Products
{
    public class InquiryProductProcessor : IInquiryProductProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IProductRepository _productRepository;
        public InquiryProductProcessor(IProductRepository productRepository, IAutoMapper autoMapper)
        {
            _productRepository = productRepository;
            _autoMapper = autoMapper;
        }

        //public Task<ProductUiModel> GetProductByIdAsync(Guid id)
        //{
        //    return Task.Run(() => _autoMapper.Map<ProductUiModel>(_productRepository.FindBy(id)));
        //}

        //public Task<ProductUiModel> GetProductByMobileAsync(string mobile)
        //{
        //    return Task.Run(() => _autoMapper.Map<ProductUiModel>(_productRepository.FindOneProductByMobile(mobile)));
        //}

        //public Task<bool> SearchIfAnyProductByLastNameOrFirstNameExistsAsync(string lastName, string firstName)
        //{
        //    return Task.Run(() =>  _productRepository.FindByFirstNameAndLastName(lastName, firstName) != null);
        //}

        //public Task<int> GetProductCountTotalsAsync()
        //{
        //  return Task.Run(() => _productRepository.FindCountTotals());
        //}
        public Task<ProductUiModel> GetProductByIdAsync(Guid id)
        {
          throw new NotImplementedException();
        }

        public Task<ProductUiModel> GetProductByMobileAsync(string mobile)
        {
          throw new NotImplementedException();
        }

        public Task<bool> SearchIfAnyProductByLastNameOrFirstNameExistsAsync(string lastName, string firstName)
        {
          throw new NotImplementedException();
        }

        public Task<int> GetProductCountTotalsAsync()
        {
          throw new NotImplementedException();
        }
    }
}