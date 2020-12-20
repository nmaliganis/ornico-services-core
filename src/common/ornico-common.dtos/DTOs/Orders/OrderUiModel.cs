using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ornico.common.dtos.DTOs.Bases;
using ornico.common.dtos.DTOs.Products;

namespace ornico.common.dtos.DTOs.Orders
{
    public class OrderUiModel : IUiModel
    {
        [Key]
        public Guid Id { get; set; }
        [Editable(true)]
        public string Message { get; set; }

        [Required]
        [Editable(true)]
        public double OrderTotals { get; set; }

        [Required]
        [Editable(true)]
        public List<ProductRetrievalUiModel> Products { get; set; }
    }
}
