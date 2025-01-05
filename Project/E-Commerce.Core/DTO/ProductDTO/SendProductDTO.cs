using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.DTO.ProductDTO
{
    public class SendProductDTO
    {

        [MaxLength(50)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Material is required")]
        public string Material { get; set; }
        [Required(ErrorMessage = "SubCategory is required")]
        public string SubCategory { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }
    }
}
