using System;
using System.Collections.Generic;
using ornico.common.dtos.DTOs.Orders;
using ornico.common.dtos.DTOs.Products;
using ornico.common.infrastructure.PropertyMappings;
using ornico.core.model.Orders;
using ornico.core.model.Products;

namespace ornico.core.api.Helpers
{
    public class PropertyMappingService : BasePropertyMapping
    {
        private readonly Dictionary<string, PropertyMappingValue> _productPropertyMapping =
          new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
          {
            {"id", new PropertyMappingValue(new List<string>() {"id"})},
            {"Name", new PropertyMappingValue(new List<string>() {"Name"})},
            {"Description", new PropertyMappingValue(new List<string>() {"Description"})},
            {"Price", new PropertyMappingValue(new List<string>() {"Price"})},
          };

        private readonly Dictionary<string, PropertyMappingValue> _orderPropertyMapping =
          new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
          {
            {"id", new PropertyMappingValue(new List<string>() {"id"})},
            {"Totals", new PropertyMappingValue(new List<string>() {"Totals"})},
            {"Comments", new PropertyMappingValue(new List<string>() {"Comments"})},
          };

        private static readonly IList<IPropertyMapping> PropertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService() : base(PropertyMappings)
        {
            PropertyMappings.Add(new PropertyMapping<ProductUiModel, Product>(_productPropertyMapping));
            PropertyMappings.Add(new PropertyMapping<OrderUiModel, Order>(_orderPropertyMapping));
        }
    }
}
