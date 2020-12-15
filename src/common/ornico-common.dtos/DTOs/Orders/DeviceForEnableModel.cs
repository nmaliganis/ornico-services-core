using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Orders
{
  public class DeviceForEnableModel
  {
    [Required]
    [Editable(true)] public bool DeviceEnableStatus{ get; set; }
  }
}