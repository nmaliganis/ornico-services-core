using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Orders
{
    public class OrderForCreationUiModel
    {
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string OrderName { get; set; }
        [Editable(true)]
        public string OrderDescription { get; set; }
        [Editable(true)]
        public double OrderPrice { get; set; }
    }
}
