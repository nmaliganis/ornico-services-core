using System;
using System.ComponentModel.DataAnnotations;
using ornico.common.dtos.DTOs.Bases;

namespace ornico.common.dtos.DTOs.Orders.Measurements
{
    public class MeasurementUiModel : IUiModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Message { get; set; }
    
        [Required]
        [Editable(true)]
        public virtual DateTime MeasurementCreatedDate { get; set; }
        [Required]
        [Editable(true)]
        public virtual DateTime MeasurementModifiedDate { get; set; }
        [Required]
        [Editable(true)]
        public virtual string MeasurementJsonbValue { get; set; }
    }
}