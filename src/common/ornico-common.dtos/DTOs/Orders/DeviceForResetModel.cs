﻿using System;
using System.ComponentModel.DataAnnotations;

namespace magic.button.common.dtos.Vms.Devices
{
  public class DeviceForResetModel
  {
    [Required]
    [Editable(true)] public Guid DeviceResetCode { get; set; }
  }
}