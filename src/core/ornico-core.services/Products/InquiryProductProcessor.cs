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

    public Task<ProductUiModel> GetProductByIdAsync(Guid id)
    {
      return Task.Run(() => _autoMapper.Map<ProductUiModel>(_productRepository.FindBy(id)));
    }
  }
}