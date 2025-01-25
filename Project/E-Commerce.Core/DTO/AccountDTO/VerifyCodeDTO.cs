using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTO.AccountDTO
{
    public class VerifyCodeDTO
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
