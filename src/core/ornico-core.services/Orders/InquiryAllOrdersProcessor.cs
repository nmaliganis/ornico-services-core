using ornico.common.infrastructure.PropertyMappings;
using ornico.common.infrastructure.TypeMappings;
using ornico.core.contracts.Orders;
using ornico.core.repository.ContractRepositories;

namespace ornico.core.services.Orders
{
  public class InquiryAllOrdersProcessor : IInquiryAllOrdersProcessor
  {
    private readonly IAutoMapper _autoMapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IPropertyMappingService _propertyMappingService;

    public InquiryAllOrdersProcessor(IAutoMapper autoMapper,
      IOrderRepository orderRepository, IPropertyMappingService propertyMappingService)
    {
      _autoMapper = autoMapper;
      _orderRepository = orderRepository;
      _propertyMappingService = propertyMappingService;
    }

    //public Task<PagedList<Order>> GetOrdersAsync(OrdersResourceParameters OrdersResourceParameters)
    //{
    //  var collectionBeforePaging =
    //    QueryableExtensions.ApplySort(_OrderRepository
    //        .FindAllOrdersPagedOf(OrdersResourceParameters.PageIndex,
    //          OrdersResourceParameters.PageSize),
    //      OrdersResourceParameters.OrderBy,
    //      _propertyMappingService.GetPropertyMapping<OrderUiModel, Order>());

    //  if (!string.IsNullOrEmpty(OrdersResourceParameters.Filter) &&
    //      !string.IsNullOrEmpty(OrdersResourceParameters.SearchQuery))
    //  {
    //    var searchQueryForWhereClauseFilterFields = OrdersResourceParameters.Filter
    //      .Trim().ToLowerInvariant();

    //    var searchQueryForWhereClauseFilterSearchQuery = OrdersResourceParameters.SearchQuery
    //      .Trim().ToLowerInvariant();

    //      collectionBeforePaging.QueriedItems = collectionBeforePaging.QueriedItems
    //        .AsEnumerable().FilterData(searchQueryForWhereClauseFilterFields, searchQueryForWhereClauseFilterSearchQuery).AsQueryable();
    //  }

    //  return Task.Run(() => PagedList<Order>.Create(collectionBeforePaging,
    //    OrdersResourceParameters.PageIndex,
    //    OrdersResourceParameters.PageSize));
    //}

    //public Task<PagedList<Order>> GetOrdersAsync(OrdersResourceParameters ordersResourceParameters)
    //{
    //  throw new System.NotImplementedException();
    //}

    //public Task<List<OrderUiModel>> GetOrdersForRoutesAsync()
    //{
    //  return Task.Run(() => _orderRepository.FindOrdersForRoutes().Select(x => _autoMapper.Map<OrderUiModel>(x)).ToList());
    //}
  }
}