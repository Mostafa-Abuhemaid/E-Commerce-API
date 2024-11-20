using AutoMapper;
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
        private readonly IMapper _mapper;
        public CategoryController(AppDBContext appContext, ICategory category, IConfiguration configuration, IMapper mapper)
        {
            _appContext = appContext;
            _category = category;
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
		public async Task<IActionResult> GetProductsByCategoryAsync([FromRoute] int id)
		{
            var products = await _category.GetProductsByCategoryIdAsync(id);
            if(products == null)
            {
                return NotFound();
            }
           
            return Ok(products);

		}


        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategoryAsync()
        {
        
            var categories = await _category.GetAllCategory();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }
            return Ok(categories);
        }

		[HttpPost]
		public async Task<IActionResult> AddCategory(SendCategoryDTO categoryDTO)
		{
            var category = await _category.CreateCategoryAsync(categoryDTO);
            if (category != null)
            {
                return Ok($"Category '{category.Name}' has been added successfully.");
            }
            return BadRequest("Failed to add the category.");

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
