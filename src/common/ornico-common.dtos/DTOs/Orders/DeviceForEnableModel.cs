using System.ComponentModel.DataAnnotations;

namespace magic.button.common.dtos.Vms.Devices
{
  public class DeviceForEnableModel
  {
    [Required]
    [Editable(true)] public bool DeviceEnableStatus{ get; set; }
  }
}