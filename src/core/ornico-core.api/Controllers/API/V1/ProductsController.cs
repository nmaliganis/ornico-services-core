using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ornico.common.dtos.DTOs.Products;
using ornico.common.dtos.Links;
using ornico.common.infrastructure.Extensions;
using ornico.common.infrastructure.PropertyMappings;
using ornico.common.infrastructure.PropertyMappings.TypeHelpers;
using ornico.core.api.Controllers.API.Base;
using ornico.core.api.Validators;
using ornico.core.contracts.Products;
using ornico.core.contracts.Users;
using ornico.core.contracts.V1;
using Serilog;

namespace ornico.core.api.Controllers.API.V1
{
  [Produces("application/json")]
  [ApiVersion("1.0")]
  [ResponseCache(Duration = 0, NoStore = true, VaryByHeader = "*")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  public class ProductsController : BaseController
  {
    private readonly IUrlHelper _urlHelper;
    private readonly ITypeHelperService _typeHelperService;
    private readonly IPropertyMappingService _propertyMappingService;

    private readonly IInquiryProductProcessor _inquiryProductProcessor;
    private readonly ICreateProductProcessor _createProductProcessor;
    private readonly IUpdateProductProcessor _updateProductProcessor;
    private readonly IDeleteProductProcessor _deleteProductProcessor;

    private readonly IInquiryUserProcessor _inquiryUserProcessor;


    public ProductsController(IUrlHelper urlHelper,
      ITypeHelperService typeHelperService,
      IPropertyMappingService propertyMappingService,
      IProductsControllerDependencyBlock blockProduct,
      IUsersControllerDependencyBlock blockUser)
    {
      _urlHelper = urlHelper;
      _typeHelperService = typeHelperService;
      _propertyMappingService = propertyMappingService;

      _inquiryProductProcessor = blockProduct.InquiryProductProcessor;
      _createProductProcessor = blockProduct.CreateProductProcessor;
      _updateProductProcessor = blockProduct.UpdateProductProcessor;
      _deleteProductProcessor = blockProduct.DeleteProductProcessor;

      _inquiryUserProcessor = blockUser.InquiryUserProcessor;
    } 
    
    /// <summary>
    /// POST : Create a New Product.
    /// </summary>
    /// <param name="productForCreationUiModel">ProductForCreationUiModel the Request Model for Creation</param>
    /// <remarks> return a ResponseEntity with status 201 (Created) if the new Product is created, 400 (Bad Request), 500 (Server Error) </remarks>
    /// <response code="201">Created (if the Product is created)</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost(Name = "PostProductRoute")]
    [ValidateModel]
    public async Task<IActionResult> PostProductRouteAsync(
      [FromBody] ProductForCreationUiModel productForCreationUiModel)
    {
      var newCreatedProduct =
        await _createProductProcessor.CreateProductAsync(productForCreationUiModel);

      switch (newCreatedProduct.Message)
      {
        case ("SUCCESS_CREATION"):
        {
          Log.Information(
            $"--Method:PostProductRouteAsync -- Message:PRODUCT_CREATION_SUCCESSFULLY -- " +
            $"Datetime:{DateTime.Now} -- ProductInfo:{productForCreationUiModel.ProductName}");
          return Created(nameof(PostProductRouteAsync), newCreatedProduct);
        }
        case ("ERROR_ALREADY_EXISTS"):
        {
          Log.Error(
            $"--Method:PostProductRouteAsync -- Message:ERROR_PRODUCT_ALREADY_EXISTS -- " +
            $"Datetime:{DateTime.Now} -- ProductInfo:{productForCreationUiModel.ProductName}");
          return BadRequest(new {errorMessage = "PRODUCT_ALREADY_EXISTS"});
        }
        case ("ERROR_PRODUCT_NOT_MADE_PERSISTENT"):
        {
          Log.Error(
            $"--Method:PostProductRouteAsync -- Message:ERROR_PRODUCT_NOT_MADE_PERSISTENT -- " +
            $"Datetime:{DateTime.Now} -- ProductInfo:{productForCreationUiModel.ProductName}");
          return BadRequest(new {errorMessage = "ERROR_CREATION_NEW_PRODUCT"});
        }
        case ("UNKNOWN_ERROR"):
        {
          Log.Error(
            $"--Method:PostProductRouteAsync -- Message:ERROR_CREATION_NEW_PRODUCT -- " +
            $"Datetime:{DateTime.Now} -- ProductInfo:{productForCreationUiModel.ProductName}");
          return BadRequest(new {errorMessage = "ERROR_CREATION_NEW_PRODUCT"});
        }
      }

      return NotFound("CREATION_FAILED");
    }

    /// <summary>
    /// Get : Retrieve Stored providing Product Id
    /// </summary>
    /// <param name="id">Product Id the Request Index for Retrieval</param>
    /// <param name="fields">Fiends to be filtered with for the returned Product</param>
    /// <remarks>Retrieve Product Role providing Id and [Optional] fields</remarks>
    /// <response code="200">Resource retrieved correctly</response>
    /// <response code="404">Resource Not Found</response>
    /// <response code="500">Internal Server Error.</response>
    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<IActionResult> GetProductAsync(Guid id, [FromQuery] string fields)
    {
      if (!_typeHelperService.TypeHasProperties<ProductUiModel>
        (fields))
      {
        return BadRequest();
      }

      var productFromRepo = await _inquiryProductProcessor.GetProductByIdAsync(id);

      if (productFromRepo == null)
      {
        return NotFound("ERROR_PRODUCT_RETRIEVAL");
      }

      var product = Mapper.Map<ProductUiModel>(productFromRepo);

      var links = CreateLinksForProduct(id, fields);

      var linkedResourceToReturn = product.ShapeData(fields)
        as IDictionary<string, object>;

      linkedResourceToReturn.Add("links", links);

      return Ok(linkedResourceToReturn);
    }

    /// <summary>
    /// PUT : Update an Existing Product.
    /// </summary>
    /// <param name="id">Product Id for Modification</param>
    /// <param name="productForModificationUiModel">ProductForModificationUiModel the Request Model for Modification</param>
    /// <remarks> return a ResponseEntity with status 201 (Created) if the new Container is created, 400 (Bad Request), 500 (Server Error) </remarks>
    /// <response code="200">Ok (if the Product is updated)</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{id}", Name = "PutProductRoute")]
    [ValidateModel]
    public async Task<IActionResult> PutProductRouteAsync(Guid id,
      [FromBody] ProductForModificationUiModel productForModificationUiModel)
    {
      var modifiedProduct =
        await _updateProductProcessor.UpdateProductAsync(id, productForModificationUiModel);

      switch (modifiedProduct.Message)
      {
        case ("SUCCESS_MODIFICATION"):
        {
          Log.Information(
            $"--Method:PutProductRouteAsync -- Message:Product_ACTIVATION_SUCCESSFULLY -- " +
            $"Datetime:{DateTime.Now} -- ProductInfo:{id} ");
          return Ok(modifiedProduct);
        }
        case ("ERROR_INVALID_PRODUCT_MODEL"):
        {
          return BadRequest(new {errorMessage = "ERROR_INVALID_PRODUCT_MODEL"});
        }     
        case ("ERROR_PRODUCT_NOT_EXIST"):
        {
          return BadRequest(new {errorMessage = "ERROR_PRODUCT_NOT_EXIST"});
        }       
        case ("ERROR_PRODUCT_NOT_MADE_PERSISTENT"):
        {
          return BadRequest(new {errorMessage = "ERROR_PRODUCT_NOT_MADE_PERSISTENT"});
        }
        case ("UNKNOWN_ERROR"):
        {
          return BadRequest(new {errorMessage = "ERROR_ACTIVATION_PRODUCT"});
        }
      }

      return NotFound("MODIFICATION_FAILED");
    }


    /// <summary>
    /// Delete - Delete an existing Product 
    /// </summary>
    /// <param name="id">Product Id for Deletion</param>
    /// <remarks>Delete Existing Product </remarks>
    /// <response code="200">Resource retrieved correctly</response>
    /// <response code="400">Resource Not Found</response>
    /// <response code="500">Internal Server Error.</response>
    [HttpDelete("{id}", Name = "DeleteProductRoot")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteHardProductRoot(Guid id)
    {
      var deletionStatus = await _deleteProductProcessor.DeleteProductAsync(id);
      //return deletionStatus ? (IActionResult) Ok("SUCCESS_CREATION") : BadRequest("ERROR_PRODUCT_DELETION");
      return Ok();
    }

    #region Link Builder

    private IEnumerable<LinkDto> CreateLinksForProduct(Guid id, string fields)
    {
      var links = new List<LinkDto>();

      if (String.IsNullOrWhiteSpace(fields))
      {
        links.Add(
          new LinkDto(_urlHelper.Link("GetProduct", new {id = id}),
            "self",
            "GET"));
      }
      else
      {
        links.Add(
          new LinkDto(_urlHelper.Link("GetProduct", new {id = id, fields = fields}),
            "self",
            "GET"));
      }

      return links;
    }

    #endregion
  }
}