using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTO
{
    public class OrderDTO
    {
        public string ShoppingAdress { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}
