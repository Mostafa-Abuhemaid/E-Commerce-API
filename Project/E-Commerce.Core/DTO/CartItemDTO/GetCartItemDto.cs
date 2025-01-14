using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTO.CartItemDTO
{
    public class GetCartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Material is required")]
        public string Material { get; set; }

        [Required(ErrorMessage = "SubCategory is required")]
        public string SubCategory { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public decimal TotalPriceForProduct { get; set; }
    }
}
