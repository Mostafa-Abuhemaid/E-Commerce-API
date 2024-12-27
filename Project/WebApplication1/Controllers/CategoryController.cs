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

        
        private readonly IUnitOfWork _unitOfWork;
   
        public CategoryController( IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }
        [HttpGet("{id}")]
		public async Task<IActionResult> GetProductsByCategoryAsync([FromRoute] int id)
		{
            var products = await _unitOfWork.CategoryService.GetProductsByCategoryIdAsync(id);
            if(products == null)
            {
                return NotFound();
            }
           
            return Ok(products);

		}


        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategoryAsync()
        {
        
            var categories = await _unitOfWork.CategoryService.GetAllCategory();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }
            return Ok(categories);
        }

		[HttpPost]
		public async Task<IActionResult> AddCategory(SendCategoryDTO categoryDTO)
		{
            var category = await _unitOfWork.CategoryService.CreateCategoryAsync(categoryDTO);
            if (category != null)
            {
                return Ok($"Category '{category.Name}' has been added successfully.");
            }
            return BadRequest("Failed to add the category.");

        }
       
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _unitOfWork.CategoryService.DeleteCategoryAsync(id);

            if (category != null)
            {
          
                return Ok("Category has deleted ");

            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]int id,SendCategoryDTO sendCategoryDTO)
        {
            var cat=_unitOfWork.CategoryService.UpdateCategoryAsync(id,sendCategoryDTO);    
            if(cat != null) 
                return Ok("Category has been Update successfuly ");
            return BadRequest();
        }
          
           
        
    }
}
