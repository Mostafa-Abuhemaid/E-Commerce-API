using E_Commerce.Core.DTO;
using E_Commerce.Core.DTO.CategoryDTO;
using E_Commerce.Core.DTO.ProductDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using ECommerce.Repository.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly AppDBContext _appContext;
		private readonly ICategory _category;
        private readonly IConfiguration _configuration;
        public CategoryController(AppDBContext appContext, ICategory category, IConfiguration configuration)
        {
            _appContext = appContext;
            _category = category;
            _configuration = configuration;
        }
        [HttpGet("{id}")]
		public async Task<IActionResult> GetProductsByCategoryAsync([FromRoute] int id)
		{
           
            var products = await _category.GetProductsByCategoryIdAsync(id);

            var productdto = products.Select(p => new GetProductDTO
			{
                Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Material = p.Material,
				Price = p.Price,
                SubCategory = p.SubCategory,
                ImagePath=p.Image,
              
               

                


			}
				).ToList();
            for (int i = 0; i < productdto.Count(); i++)
            {
                productdto[i].ImagePath = $"{_configuration["BaseURL"]}/Images/Product/{productdto[i].ImagePath}";

            }
            return Ok(productdto);


		}
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategoryAsync()
        {
            var Categories = _appContext.Categories.ToList();
            var CategoriesDTO = Categories.Select(c => new GetCategoryDTO
            {
                Id= c.Id,
               Name=c.Name,
               imgURL=c.ImgeURL
            }

                ).ToList();
            for(int i=0;i<CategoriesDTO.Count();i++)
            {
                CategoriesDTO[i].imgURL = $"{_configuration["BaseURL"]}/Images/Category/{CategoriesDTO[i].imgURL}";
            }
            return Ok(CategoriesDTO);
        }
		[HttpPost]
		public async Task<IActionResult> AddCategory(SendCategoryDTO categoryDTO)
		{
            if (ModelState.IsValid)
            {
                var imgname = Files.UploadFile(categoryDTO.formFile, "Category");
                Category cat = new Category
                {
                    Name = categoryDTO.Name,
                ImgeURL=imgname

                };
                await _appContext.Categories.AddAsync(cat);
                await _appContext.SaveChangesAsync();
                return Ok("Category has be Created ");
			}
			return BadRequest();
			
		}
       
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _appContext.Categories.FindAsync(id);

            if (category != null)
            {
                _appContext.Categories.Remove(category);
                await _appContext.SaveChangesAsync();
                Files.DeleteFile(category.ImgeURL, "Category");
                return Ok("Category has deleted ");

            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]int id,SendCategoryDTO sendCategoryDTO)
        {
          var Cat= await _appContext.Categories.FindAsync(id);
            if (Cat != null)
            {
                var imgname = Files.UploadFile(sendCategoryDTO.formFile, "Category");

                Cat.Name = sendCategoryDTO.Name;
                Cat.ImgeURL = imgname;

 await _appContext.SaveChangesAsync();
                return Ok("Update");
            }
            return NotFound();
           
        }
    }
}
