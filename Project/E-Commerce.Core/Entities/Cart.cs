using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Core.Identity;

namespace E_Commerce.Core.Entities
{
    public  class Cart
	{
		public int Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        [ForeignKey(nameof(ApplicationUser))]
		public string UserAppId { get; set; }
		public ApplicationUser UserApp { get; set; }
        public decimal TotalAmount => CalculateTotal(); 

        private decimal CalculateTotal()
        {
            return Items.Sum(item => item.Product.Price * item.Quantity); 
        }
    } 
}
