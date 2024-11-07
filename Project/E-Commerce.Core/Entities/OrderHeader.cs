using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities
{
	public class OrderHeader
	{
		public int Id { get; set; }
		public DateTime DateTime { get; set; } = DateTime.Now;
		public string status { get; set; }

		public decimal DeliveryCost { get; set; } = 60;
		public Decimal subtotal{ get; set; }
		public decimal TotalPrice { get => DeliveryCost + subtotal; }


		public IEnumerable<OrderDetails> OrderItems { get; set; }= new List<OrderDetails>();
	}
}
