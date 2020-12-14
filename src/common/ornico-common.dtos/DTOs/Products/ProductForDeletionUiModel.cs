using System;
using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Products
{
  public class ProductForDeletionUiModel
  {
    [Required]
    [Editable(true)]
    public Guid Id { get; set; }
    [Required]
    [Editable(true)]
    public bool DeletionStatus { get; set; }
    [Required]
    [Editable(true)]
    public string Message { get; set; }
  }
}