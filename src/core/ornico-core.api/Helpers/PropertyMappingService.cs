using System;
using System.Collections.Generic;
using ornico.common.dtos.DTOs.Products;
using ornico.common.infrastructure.PropertyMappings;
using ornico.core.model.Products;

namespace ornico.core.api.Helpers
{
    public class PropertyMappingService : BasePropertyMapping
    {
        private readonly Dictionary<string, PropertyMappingValue> _productPropertyMapping =
          new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
          {
            {"id", new PropertyMappingValue(new List<string>() {"id"})},
            {"Firstname", new PropertyMappingValue(new List<string>() {"Firstname"})},
            {"Lastname", new PropertyMappingValue(new List<string>() {"Lastname"})},
            {"Mobile", new PropertyMappingValue(new List<string>() {"Mobile"})},
            {"IsActive", new PropertyMappingValue(new List<string>() {"IsActive"})},
          };

        private static readonly IList<IPropertyMapping> PropertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService() : base(PropertyMappings)
        {
            PropertyMappings.Add(new PropertyMapping<ProductUiModel, Product>(_productPropertyMapping));
        }
    }
}
