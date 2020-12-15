using System;
using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Orders
{
  public class DeviceForMeasurementModel
  {
    [Required]
    [Editable(true)]   
    public string DeviceId { get; set; }
    [Required]
    [Editable(true)]   
    public Guid CorrelationId { get; set; }
    [Required]
    [Editable(true)]   
    public DateTime Timestamp { get; set; }
    [Required]
    [Editable(true)]   
    public int ButtonStatus { get; set; }
    [Required]
    [Editable(true)]   
    public double BatValue { get; set; }
    [Required]
    [Editable(true)]   
    public double TempValue { get; set; }
    [Required]
    [Editable(true)]   
    public string  Rssi { get; set; }
    [Required]
    [Editable(true)]   
    public string  Snr { get; set; }
    [Required]
    [Editable(true)]   
    public string MeasurementValueJson { get; set; }
  }
}