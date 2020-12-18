using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Orders
{
    public class OrderForCreationUiModel
    {
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public IList<OrderCreationUIModel> Products { get; set; }
    }

    public class OrderCreationUIModel
    {
      [Required]
      [Editable(true)]
      public Guid ProductId { get; set; }
      [Required]
      [Editable(true)]
      public int ProductQty { get; set; }
    }
}
