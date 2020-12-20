using System;
using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Users
{
    public class UserForAllRetrievalUiModel
    {
        [Required]
        [Editable(true)]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Login { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Email { get; set; }
    }
}