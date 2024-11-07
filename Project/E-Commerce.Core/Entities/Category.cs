using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities
{
	public class Category
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }
		public string ImgeURL { get; set; }
		
		public IEnumerable<Product> Products { get; set; }	
	}
}
