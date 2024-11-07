using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTO
{
    public class UserDTO
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }
    }
}
