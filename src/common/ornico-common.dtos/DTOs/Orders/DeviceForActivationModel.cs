using System;
using System.ComponentModel.DataAnnotations;

namespace magic.button.common.dtos.Vms.Devices
{
  public class DeviceForActivationModel
  {
    [Required]
    [Editable(true)] public Guid DeviceActivationCode { get; set; }
  }
}