using System;
using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Orders
{
  public class DeviceForResetModel
  {
    [Required]
    [Editable(true)] public Guid DeviceResetCode { get; set; }
  }
}