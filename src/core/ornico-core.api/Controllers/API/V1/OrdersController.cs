using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ornico.common.dtos.DTOs.Orders;
using ornico.common.dtos.Links;
using ornico.common.infrastructure.Extensions;
using ornico.common.infrastructure.Helpers;
using ornico.common.infrastructure.Helpers.ResourceParameters;
using ornico.common.infrastructure.PropertyMappings;
using ornico.common.infrastructure.PropertyMappings.TypeHelpers;
using ornico.core.api.Controllers.API.Base;
using ornico.core.api.Validators;
using ornico.core.contracts.Orders;
using ornico.core.contracts.Users;
using ornico.core.contracts.V1;
using ornico.core.model.Orders;

namespace ornico.core.api.Controllers.API.V1
{
  [Produces("application/json")]
  [ApiVersion("1.0")]
  [ResponseCache(Duration = 0, NoStore = true, VaryByHeader = "*")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  //[Authorize]
  public class OrdersController : BaseController
  {
    private readonly IUrlHelper _urlHelper;
    private readonly ITypeHelperService _typeHelperService;
    private readonly IPropertyMappingService _propertyMappingService;

    private readonly IInquiryAllOrdersProcessor _inquiryAllOrdersProcessor;
    private readonly IInquiryOrderProcessor _inquiryOrderProcessor;
    private readonly ICreateOrderProcessor _createOrderProcessor;
    private readonly IUpdateOrderProcessor _updateOrderProcessor;
    private readonly IDeleteOrderProcessor _deleteOrderProcessor;

    private readonly IInquiryUserProcessor _inquiryUserProcessor;


    public OrdersController(IUrlHelper urlHelper,
      ITypeHelperService typeHelperService,
      IPropertyMappingService propertyMappingService,
      IOrdersControllerDependencyBlock blockOrder,
      IUsersControllerDependencyBlock blockUser)
    {
      _urlHelper = urlHelper;
      _typeHelperService = typeHelperService;
      _propertyMappingService = propertyMappingService;

      _inquiryAllOrdersProcessor = blockOrder.InquiryAllOrdersProcessor;
      _inquiryOrderProcessor = blockOrder.InquiryOrderProcessor;
      _createOrderProcessor = blockOrder.CreateOrderProcessor;
      _updateOrderProcessor = blockOrder.UpdateOrderProcessor;
      _deleteOrderProcessor = blockOrder.DeleteOrderProcessor;

      _inquiryUserProcessor = blockUser.InquiryUserProcessor;
    } 
    
    /// <summary>
    /// POST : Create a New Order.
    /// </summary>
    /// <param name="orderForCreationUiModel">OrderForCreationUiModel the Request Model for Creation</param>
    /// <remarks> return a ResponseEntity with status 201 (Created) if the new Order is created, 400 (Bad Request), 500 (Server Error) </remarks>
    /// <response code="201">Created (if the Order is created)</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost(Name = "PostOrderRoute")]
    [ValidateModel]
    public async Task<IActionResult> PostOrderRouteAsync(
      [FromBody] OrderForCreationUiModel orderForCreationUiModel)
    {
      var userAudit = await _inquiryUserProcessor.GetUserByLoginAsync(GetEmailFromClaims());

      if (userAudit == null)
        return BadRequest();

      var newCreatedOrder =
        await _createOrderProcessor.CreateOrderAsync(orderForCreationUiModel);

      //switch (newCreatedOrder.Message)
      //{
      //  case ("SUCCESS_CREATION"):
      //  {
      //    Log.Information(
      //      $"--Method:PostOrderRouteAsync -- Message:Order_CREATION_SUCCESSFULLY -- " +
      //      $"Datetime:{DateTime.Now} -- OrderInfo:{orderForCreationUiModel.OrderName}");
      //    return Created(nameof(PostOrderRouteAsync), newCreatedOrder);
      //  }
      //  case ("ERROR_ALREADY_EXISTS"):
      //  {
      //    Log.Error(
      //      $"--Method:PostOrderRouteAsync -- Message:ERROR_Order_ALREADY_EXISTS -- " +
      //      $"Datetime:{DateTime.Now} -- OrderInfo:{orderForCreationUiModel.OrderName}");
      //    return BadRequest(new {errorMessage = "Order_ALREADY_EXISTS"});
      //  }
      //  case ("ERROR_Order_NOT_MADE_PERSISTENT"):
      //  {
      //    Log.Error(
      //      $"--Method:PostOrderRouteAsync -- Message:ERROR_Order_NOT_MADE_PERSISTENT -- " +
      //      $"Datetime:{DateTime.Now} -- OrderInfo:{orderForCreationUiModel.OrderName}");
      //    return BadRequest(new {errorMessage = "ERROR_CREATION_NEW_Order"});
      //  }
      //  case ("UNKNOWN_ERROR"):
      //  {
      //    Log.Error(
      //      $"--Method:PostOrderRouteAsync -- Message:ERROR_CREATION_NEW_Order -- " +
      //      $"Datetime:{DateTime.Now} -- OrderInfo:{orderForCreationUiModel.OrderName}");
      //    return BadRequest(new {errorMessage = "ERROR_CREATION_NEW_Order"});
      //  }
      //}

      return NotFound();
    }

    /// <summary>
    /// Get : Retrieve Stored providing Order Id
    /// </summary>
    /// <param name="id">Order Id the Request Index for Retrieval</param>
    /// <param name="fields">Fiends to be filtered with for the returned Order</param>
    /// <remarks>Retrieve Order Role providing Id and [Optional] fields</remarks>
    /// <response code="200">Resource retrieved correctly</response>
    /// <response code="404">Resource Not Found</response>
    /// <response code="500">Internal Server Error.</response>
    [HttpGet("{id}", Name = "GetOrder")]
    public async Task<IActionResult> GetOrderAsync(Guid id, [FromQuery] string fields)
    {
      if (!_typeHelperService.TypeHasProperties<OrderUiModel>
        (fields))
      {
        return BadRequest();
      }

      var orderFromRepo = await _inquiryOrderProcessor.GetOrderByIdAsync(id);

      if (orderFromRepo == null)
      {
        return NotFound();
      }

      var order = Mapper.Map<OrderUiModel>(orderFromRepo);

      var links = CreateLinksForOrder(id, fields);

      var linkedResourceToReturn = order.ShapeData(fields)
        as IDictionary<string, object>;

      linkedResourceToReturn.Add("links", links);

      return Ok(linkedResourceToReturn);
    }

    /// <summary>
    /// Get : Retrieve All/or Partial Paged Stored Orders 
    /// </summary>
    /// <remarks>Retrieve paged Orders providing Paging Query</remarks>
    /// <response code="200">Resource retrieved correctly.</response>
    /// <response code="500">Internal Server Error.</response>
    [HttpGet(Name = "GetOrders")]
    public async Task<IActionResult> GetOrdersAsync([FromQuery] OrdersResourceParameters ordersResourceParameters)
    {
      if (!_propertyMappingService.ValidMappingExistsFor<OrderUiModel, Order>
        (ordersResourceParameters.OrderBy))
      {
        return BadRequest();
      }

      if (!_typeHelperService.TypeHasProperties<OrderUiModel>
        (ordersResourceParameters.Fields))
      {
        return BadRequest();
      }

      //var ordersQueryable = 
      //  //await _inquiryAllOrdersProcessor.GetOrdersAsync(ordersResourceParameters);

      //var orders = Mapper.Map<IEnumerable<OrderUiModel>>(ordersQueryable);

      //var previousPageLink = ordersQueryable.HasPrevious
      //  ? CreateOrdersResourceUri(ordersResourceParameters,
      //    ResourceUriType.PreviousPage)
      //  : null;

      //var nextPageLink = ordersQueryable.HasNext
      //  ? CreateOrdersResourceUri(ordersResourceParameters, ResourceUriType.NextPage)
      //  : null;

      //var paginationMetadata = new
      //{
      //  previousPageLink = previousPageLink,
      //  nextPageLink = nextPageLink,
      //  totalCount = ordersQueryable.TotalCount,
      //  pageSize = ordersQueryable.PageSize,
      //  currentPage = ordersQueryable.CurrentPage,
      //  totalPages = ordersQueryable.TotalPages
      //};

      //Response.Headers.Add("X-Pagination",
      //  JsonConvert.SerializeObject(paginationMetadata));

      //return Ok(orders.ShapeData(ordersResourceParameters.Fields));
      return Ok();
    }

    #region Link Builder

    private IEnumerable<LinkDto> CreateLinksForOrder(Guid id, string fields)
    {
      var links = new List<LinkDto>();

      if (String.IsNullOrWhiteSpace(fields))
      {
        links.Add(
          new LinkDto(_urlHelper.Link("GetOrder", new {id = id}),
            "self",
            "GET"));
      }
      else
      {
        links.Add(
          new LinkDto(_urlHelper.Link("GetOrder", new {id = id, fields = fields}),
            "self",
            "GET"));
      }

      return links;
    }


    private IEnumerable<LinkDto> CreateLinksForOrders(
      OrdersResourceParameters ordersResourceParameters,
      bool hasNext, bool hasPrevious)
    {
      var links = new List<LinkDto>
      {
        new LinkDto(CreateOrdersResourceUri(ordersResourceParameters,
            ResourceUriType.Current)
          , "self", "GET")
      };

      if (hasNext)
      {
        links.Add(
          new LinkDto(CreateOrdersResourceUri(ordersResourceParameters,
              ResourceUriType.NextPage),
            "nextPage", "GET"));
      }

      if (hasPrevious)
      {
        links.Add(
          new LinkDto(CreateOrdersResourceUri(ordersResourceParameters,
              ResourceUriType.PreviousPage),
            "previousPage", "GET"));
      }

      return links;
    }

    private string CreateOrdersResourceUri(OrdersResourceParameters ordersResourceParameters,
      ResourceUriType type)
    {
      switch (type)
      {
        case ResourceUriType.PreviousPage:
          return _urlHelper.Link("GetOrders",
            new
            {
              fields = ordersResourceParameters.Fields,
              orderBy = ordersResourceParameters.OrderBy,
              searchQuery = ordersResourceParameters.SearchQuery,
              pageNumber = ordersResourceParameters.PageIndex - 1,
              pageSize = ordersResourceParameters.PageSize
            });
        case ResourceUriType.NextPage:
          return _urlHelper.Link("GetOrders",
            new
            {
              fields = ordersResourceParameters.Fields,
              orderBy = ordersResourceParameters.OrderBy,
              searchQuery = ordersResourceParameters.SearchQuery,
              pageNumber = ordersResourceParameters.PageIndex + 1,
              pageSize = ordersResourceParameters.PageSize
            });
        case ResourceUriType.Current:
        default:
          return _urlHelper.Link("GetOrders",
            new
            {
              fields = ordersResourceParameters.Fields,
              orderBy = ordersResourceParameters.OrderBy,
              searchQuery = ordersResourceParameters.SearchQuery,
              pageNumber = ordersResourceParameters.PageIndex,
              pageSize = ordersResourceParameters.PageSize
            });
      }
    }

    #endregion
  }
}