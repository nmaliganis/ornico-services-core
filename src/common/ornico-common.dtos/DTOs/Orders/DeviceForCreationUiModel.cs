using System;
using System.ComponentModel.DataAnnotations;

namespace magic.button.common.dtos.Vms.Devices
{
  public class DeviceForCreationUiModel
  {
    [Required(AllowEmptyStrings = false)]
    [Editable(true)]
    public string DeviceSerialNumber { get; set; }
  }
}