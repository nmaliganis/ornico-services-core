using System;
using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Orders
{
  public class DeviceForActivationModel
  {
    [Required]
    [Editable(true)] public Guid DeviceActivationCode { get; set; }
  }
}