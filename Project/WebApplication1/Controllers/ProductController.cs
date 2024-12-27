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
        private readonly AppDBContext _appContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public readonly IProductService _product;

        public ProductController(AppDBContext appContext, IConfiguration configuration, IMapper mapper, IProductService product)
        {
            _appContext = appContext;
            _configuration = configuration;
            _mapper = mapper;
            _product = product;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _appContext.Products.Include(c => c.Category).ToListAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound("No products found.");
            }

            var productDTOs = _mapper.Map<List<GetProductDTO>>(products);
            for (int i = 0; i < productDTOs.Count(); i++)
            {
                productDTOs[i].ImagePath = $"{_configuration["BaseURL"]}/Images/Product/{productDTOs[i].ImagePath}";

            }
            return Ok(productDTOs);
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> getProductByIdAsync([FromRoute] int id)
        {

            var pro = await _product.GetProductByIdAsync(id);

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

                var product = await _product.CreateProductAsync(productDTO);
                return Ok($"{product.Name} has been Created");
            }
            return BadRequest();
        }
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var pro = await _appContext.Products.SingleOrDefaultAsync(p => p.Id == id);

            if (pro == null) return NotFound();
            await _product.DeleteProductAsync(id);

            return Ok("Product has been Deleted");
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string name)
        {
            var query = _appContext.Products.Include(c => c.Category).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }
            var products = await query.ToListAsync();
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            for (int i = 0; i < products.Count(); i++)
            {
                products[i].Image = $"{_configuration["BaseURL"]}/Images/Product/{products[i].Image}";

            }
            return Ok(products);
        }
    }
}