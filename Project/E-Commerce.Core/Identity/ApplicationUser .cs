using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Phone]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone {  get; set; }
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public Gender gender { get; set; }

    }
}
