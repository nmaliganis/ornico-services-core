using System;
using ornico.common.dtos.DTOs.Bases;

namespace ornico.common.dtos.DTOs.Products
{
    public class ProductForDeletionUiModel :  IUiModel
    {
      public Guid Id { get; set; }
      public string Message { get; set; }
      public bool DeletionStatus { get; set; }
    }
}
