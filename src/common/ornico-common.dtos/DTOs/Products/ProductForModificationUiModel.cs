using System;
using System.ComponentModel.DataAnnotations;
using ornico.common.dtos.DTOs.Bases;

namespace ornico.common.dtos.DTOs.Products
{
  public class ProductForModificationUiModel
  {
    [Editable(true)] public string ProductName { get; set; }
    [Editable(true)] public string ProductDescription { get; set; }
    [Editable(true)] public double ProductPrice { get; set; }
  }
}
