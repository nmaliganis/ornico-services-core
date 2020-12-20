using System;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Orders;
using ornico.common.infrastructure.TypeMappings;
using ornico.common.infrastructure.UnitOfWorks;
using ornico.core.contracts.Orders;
using ornico.core.model.Orders;
using ornico.core.repository.ContractRepositories;
using Serilog;

namespace ornico.core.services.Orders
{
  public class CreateOrderProcessor : ICreateOrderProcessor
  {
    private readonly IUnitOfWork _uOf;
    private readonly IUserRepository _userRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IAutoMapper _autoMapper;

    public CreateOrderProcessor(IUnitOfWork uOf, IAutoMapper autoMapper,  IUserRepository userRepository, IOrderRepository orderRepository, IProductRepository productRepository)
    {
      _uOf = uOf;
      _orderRepository = orderRepository;
      _productRepository = productRepository;
      _autoMapper = autoMapper;
      _userRepository = userRepository;
    }


    public Task<OrderUiModel> CreateOrderAsync(Guid idUser, OrderForCreationUiModel orderForCreationUiModel)
    {
      var response =
        new OrderUiModel()
        {
          Message = "START_CREATION"
        };

      if (orderForCreationUiModel == null)
      {
        response.Message = "ERROR_INVALID_ORDER_MODEL";
        return Task.Run(() => response);
      }

      var orderToBeCreated = new Order();

      try
      {
        orderToBeCreated.InjectWithInitialAttributes(
          $"New order for Products:{orderForCreationUiModel.Products.Count}");

        var userToBeInjected = _userRepository.FindBy(idUser);

        orderToBeCreated.InjectWithUser(userToBeInjected);

        foreach (var product in orderForCreationUiModel.Products)
        {
          var productToBeInjected = _productRepository.FindBy(product.ProductId);
          if(productToBeInjected != null)
          {
            OrderItem newOrderItemToBeInjected = new OrderItem()
            {
              Product = productToBeInjected,
              Quantity = product.ProductQty
            };

            orderToBeCreated.InjectedWithOrderItem(newOrderItemToBeInjected);
            orderToBeCreated.CalcNewTotals(product.ProductQty, productToBeInjected.Price);

          }
        }

        MakeOrderPersistent(orderToBeCreated);

        response = _autoMapper.Map<OrderUiModel>(_orderRepository.FindOrderForUserByOrderId(idUser, orderToBeCreated.Id));
        response.Message = "SUCCESS_CREATION";
      }
      catch (Exception exxx)
      {
        response.Message = "UNKNOWN_ERROR";
        Log.Error(
          $"Create Order" +
          $"--CreateOrder--  @fail@ [CreateOrderProcessor]. " +
          $"@innerfault:{exxx.Message} and {exxx.InnerException}");
      }

      return Task.Run(() => response);
    }

    private void MakeOrderPersistent(Order orderToBeMadePersistence)
    {
      _orderRepository.Save(orderToBeMadePersistence);
      _uOf.Commit();
    }
  }
}
