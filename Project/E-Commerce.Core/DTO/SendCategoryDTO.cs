using Microsoft.AspNetCore.Http;

namespace WebApplication1.DTO
{
	public class SendCategoryDTO
	{
       
        public string Name { get; set; }
		public IFormFile formFile { get; set; }

	}
}
