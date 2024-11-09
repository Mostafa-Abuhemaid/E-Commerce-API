using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.DTO.ProductDTO
{
    public class SendProductDTO
    {

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public string Material { get; set; }
        public string SubCategory { get; set; }

        public int CategoryId { get; set; }
    }
}
