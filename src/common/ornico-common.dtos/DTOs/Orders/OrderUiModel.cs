using System;
using System.ComponentModel.DataAnnotations;
using ornico.common.dtos.DTOs.Bases;

namespace ornico.common.dtos.DTOs.Orders
{
    public class OrderUiModel : IUiModel
    {
        [Key]
        public Guid Id { get; set; }
        [Editable(true)]
        public string Message { get; set; }

    
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string OrderName { get; set; }
    }
}
