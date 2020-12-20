using System;
using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Users
{
    public class UserUiModel
    {
        [Editable(true)]
        public string Message { get; set; }

        [Required]
        [Editable(true)]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string DisplayName { get; set; }
        [Editable(true)]
        public string Address { get; set; }
        [Editable(true)]
        public DateTime CreatedDate { get; set; }
        [Editable(true)]
        public DateTime CurrentDate { get; set; }
    }
}