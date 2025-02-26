using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTO.AccountDTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is Required")]
        [Phone]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Location is Required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public Gender gender { get; set; }
    }
}
