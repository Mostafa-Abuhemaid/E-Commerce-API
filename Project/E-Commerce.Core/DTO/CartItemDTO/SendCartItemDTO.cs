using E_Commerce.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTO.CartItemDTO
{
    public class SendCartItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Size Size { get; set; }

    }
}
