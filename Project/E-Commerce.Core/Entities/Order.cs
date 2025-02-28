using E_Commerce.Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public DateTime DateTime { get; set; } = DateTime.Now;
		public string status { get; set; }

		public decimal DeliveryCost { get; set; } = 60;
		public Decimal subtotal{ get; set; }
		public decimal TotalPrice { get => DeliveryCost + subtotal; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserAppId { get; set; }
        public ApplicationUser UserApp { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
