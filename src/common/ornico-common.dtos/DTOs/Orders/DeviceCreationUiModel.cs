using System;
using System.ComponentModel.DataAnnotations;
using ornico.common.dtos.DTOs.Bases;

namespace magic.button.common.dtos.Vms.Devices
{
  public class DeviceCreationUiModel : IUiModel
  {
    [Key] public Guid Id { get; set; }

    public string Message { get; set; }
    [Required]
    [Editable(true)]
    public virtual string DeviceSerialNumber { get; set; }
    [Required]
    [Editable(true)]
    public virtual Guid DeviceActivationCode { get; set; }
    [Required]
    [Editable(true)]
    public virtual Guid DeviceProvisioningCode { get; set; }
    [Required]
    [Editable(true)]
    public virtual Guid DeviceResetCode { get; set; }
    [Required]
    [Editable(true)]
    public virtual DateTime DeviceCreatedDate { get; set; }
    [Required]
    [Editable(true)]
    public virtual Guid DeviceCreatedBy { get; set; }
    [Required]
    [Editable(true)]
    public virtual bool DeviceIsActivated { get; set; }
    [Required]
    [Editable(true)]
    public virtual bool DeviceIsEnabled { get; set; }
    [Required]
    [Editable(true)]
    public virtual bool DeviceIsActive { get; set; }
  }
}