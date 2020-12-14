﻿using System;
using System.ComponentModel.DataAnnotations;

namespace magic.button.common.dtos.Vms.Devices
{
  public class DeviceForDeletionUiModel
  {
    [Required]
    [Editable(true)]
    public Guid Id { get; set; }
    [Required]
    [Editable(true)]
    public bool IsActive { get; set; }
    [Required]
    [Editable(true)]
    public bool DeletionStatus { get; set; }
    [Required]
    [Editable(true)]
    public string Message { get; set; }
  }
}