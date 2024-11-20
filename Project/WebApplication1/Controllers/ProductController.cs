using AutoMapper;
using E_Commerce.Core.DTO.ProductDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using ECommerce.Repository.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public ProductController(AppDBContext appContext, IConfiguration configuration, IMapper mapper)
        {
            _appContext = appContext;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _appContext.Products.Include(c=>c.Category).ToListAsync();

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
		public async Task<IActionResult> getProductByIdAsync([FromRoute]int id)
		{

            var pro = await _appContext.Products.Include(p=>p.Category).FirstOrDefaultAsync(x => x.Id == id);
		
			if (ModelState.IsValid)
			{
				var Url = $"{_configuration["BaseURL"]}/Images/Product/{pro.Image}";
                var productDTOs = _mapper.Map<GetProductDTO>(pro);

                productDTOs.ImagePath = Url;
                return Ok(productDTOs);
			}
			return NotFound();
		}
        
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string name)
        {
            var query = _appContext.Products.Include(c=>c.Category).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }
            var products = await query.ToListAsync();
            if (products == null || !products.Any())
            {
                return NotFound();
            }
			for(int i=0;i<products.Count();i++)
			{
				products[i].Image= $"{_configuration["BaseURL"]}/Images/Product/{products[i].Image}";

            }
            return Ok(products);
        }

        [HttpPost]
		public async Task<IActionResult> CreateProductAsync([FromForm] SendProductDTO productDTO)
		{
			if (ModelState.IsValid)
			{
				var imgname = Files.UploadFile(productDTO.Image, "Product");
				Product pro = new Product
				{
					Name = productDTO.Name,
					Price = productDTO.Price,
					Material = productDTO.Material,
					Description = productDTO.Description,
					Image = imgname,
					SubCategory= productDTO.SubCategory,
					CategoryId = productDTO.CategoryId

				};
				await _appContext.Products.AddAsync(pro);
				await _appContext.SaveChangesAsync();
				return Ok();
			}
			return BadRequest();
		}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var pro = await _appContext.Products.SingleOrDefaultAsync(p => p.Id == id);

            if (pro == null) return NotFound(); 

           
            Files.DeleteFile(pro.Image, "Product");
            _appContext.Products.Remove(pro);
            await _appContext.SaveChangesAsync();

            return Ok();
        }

    }
}