using Microsoft.AspNetCore.Http;

namespace E_Commerce.Core.DTO.CategoryDTO
{
    public class SendCategoryDTO
    {

        public string Name { get; set; }
        public IFormFile Image { get; set; }

    }
}
