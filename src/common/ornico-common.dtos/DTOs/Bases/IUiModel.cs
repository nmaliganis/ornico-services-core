using System;
using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Bases
{
    public interface IUiModel
    {
        [Key]
        Guid Id { get; set; }
        [Editable(false)]
        string Message { get; set; }
    }
}
