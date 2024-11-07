using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities
{
    public class CartItem
    {
        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; } 
        public Product Product { get; set; } 

        public int Quantity { get; set; }

        public decimal TotalPriceForProduct => CalculateTotal();

        private decimal CalculateTotal()
        {
            return Product.Price * Quantity;
        }
    }
}
