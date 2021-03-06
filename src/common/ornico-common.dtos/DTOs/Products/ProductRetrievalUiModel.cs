﻿using System;
using System.ComponentModel.DataAnnotations;
using ornico.common.dtos.DTOs.Bases;

namespace ornico.common.dtos.DTOs.Products
{
    public class ProductRetrievalUiModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string ProductName { get; set; }
        [Required]
        [Editable(true)]
        public double ProductPrice { get; set; }
        [Required]
        [Editable(true)]
        public int ProductQty { get; set; }
    }
}
