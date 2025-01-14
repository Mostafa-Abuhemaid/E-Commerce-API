using AutoMapper;
using E_Commerce.Core.DTO.ProductDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using ECommerce.Repository.Helper;
using ECommerce.Repository.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
    
        public readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _unitOfWork.ProductService.GetAllProductsAsync();

            if (products == null || products.Count == 0)
            
                return NotFound("No products found.");
           else
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getProductByIdAsync([FromRoute] int id)
        {

            var pro = await _unitOfWork.ProductService.GetProductByIdAsync(id);

            if (ModelState.IsValid)
            {

                return Ok(pro);
            }
            return NotFound();
        }



        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromForm] SendProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {

                var product = await _unitOfWork.ProductService.CreateProductAsync(productDTO);
                return Ok($"{product.Name} has been Created");
            }
            return BadRequest();
        }
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var pro = await _unitOfWork.ProductService.DeleteProductAsync(id);

            if (pro == null) return NotFound();

            return Ok("Product has been Deleted");
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string name)
        {
          var products= _unitOfWork.ProductService.SearchProducts(name);
            if(products == null) return NotFound(); 
            return Ok(products);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute]int id,[FromForm] SendProductDTO product)
        {
            var pro = _unitOfWork.ProductService.UpdateProductAsync(id, product);
            if (pro == null) return NotFound();
            return Ok($"Product {id} has been Update successfuly");
        }
    }
}