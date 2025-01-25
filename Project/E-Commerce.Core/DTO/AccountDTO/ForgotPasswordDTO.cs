using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTO.AccountDTO
{
    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]

        public string Email { get; set; }
    }
}
