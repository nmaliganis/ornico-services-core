using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Orders
{
  public class DeviceForCreationUiModel
  {
    [Required(AllowEmptyStrings = false)]
    [Editable(true)]
    public string DeviceSerialNumber { get; set; }
  }
}