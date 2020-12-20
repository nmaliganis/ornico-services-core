using System;
using System.ComponentModel.DataAnnotations;

namespace ornico.common.dtos.DTOs.Accounts
{
    public class UserForRegistrationUiModel
    {
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Displayname { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Username { get; set; }
        [MinLength(8)]
        [MaxLength(16)]
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Email { get; set; }
        [Editable(true)]
        public string Address { get; set; }
    }
}
