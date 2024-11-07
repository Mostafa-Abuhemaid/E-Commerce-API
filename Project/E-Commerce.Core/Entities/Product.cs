using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities
{
	public class Product
	{
		public int Id { get; set; }
		[MaxLength(50)]
		[Required(ErrorMessage ="Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Price is required")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Image is required")]
		public string Image { get; set; }
		public string Description { get; set; }

		[Required(ErrorMessage = "Material is required")]
		public string Material { get; set; }
        public string SubCategory { get; set; } 

		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
