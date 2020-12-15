using System.Linq;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Products;
using ornico.common.infrastructure.Extensions;
using ornico.common.infrastructure.Helpers.ResourceParameters;
using ornico.common.infrastructure.Paging;
using ornico.common.infrastructure.PropertyMappings;
using ornico.common.infrastructure.TypeMappings;
using ornico.core.contracts.Products;
using ornico.core.model.Products;
using ornico.core.repository.ContractRepositories;

namespace ornico.core.services.Products
{
  public class InquiryAllProductsProcessor : IInquiryAllProductsProcessor
  {
    private readonly IAutoMapper _autoMapper;
    private readonly IProductRepository _productRepository;
    private readonly IPropertyMappingService _propertyMappingService;

    public InquiryAllProductsProcessor(IAutoMapper autoMapper,
      IProductRepository productRepository, IPropertyMappingService propertyMappingService)
    {
      _autoMapper = autoMapper;
      _productRepository = productRepository;
      _propertyMappingService = propertyMappingService;
    }

    public Task<PagedList<Product>> GetProductsAsync(ProductsResourceParameters productsResourceParameters)
    {
      var collectionBeforePaging =
        QueryableExtensions.ApplySort(_productRepository
            .FindAllProductsPagedOf(productsResourceParameters.PageIndex,
              productsResourceParameters.PageSize),
          productsResourceParameters.OrderBy,
          _propertyMappingService.GetPropertyMapping<ProductUiModel, Product>());

      if (!string.IsNullOrEmpty(productsResourceParameters.Filter) &&
          !string.IsNullOrEmpty(productsResourceParameters.SearchQuery))
      {
        var searchQueryForWhereClauseFilterFields = productsResourceParameters.Filter
          .Trim().ToLowerInvariant();

        var searchQueryForWhereClauseFilterSearchQuery = productsResourceParameters.SearchQuery
          .Trim().ToLowerInvariant();

        collectionBeforePaging.QueriedItems = collectionBeforePaging.QueriedItems
          .AsEnumerable().FilterData(searchQueryForWhereClauseFilterFields, searchQueryForWhereClauseFilterSearchQuery)
          .AsQueryable();
      }

      return Task.Run(() => PagedList<Product>.Create(collectionBeforePaging,
        productsResourceParameters.PageIndex,
        productsResourceParameters.PageSize));
    }
  }
}