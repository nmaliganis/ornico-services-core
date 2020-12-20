using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Products
{
    public class ProductForCreationUiModel
    {
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string ProductName { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string ProductDescription { get; set; }
        [Required]
        [Editable(true)]
        public double ProductPrice { get; set; }
    }
}
