using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTO
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
