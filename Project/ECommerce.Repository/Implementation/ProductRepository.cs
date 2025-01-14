using AutoMapper;
using E_Commerce.Core.DTO.CategoryDTO;
using E_Commerce.Core.DTO.ProductDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using ECommerce.Repository.Helper;
using ECommerce.Repository.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Implementation
{
    public class ProductRepository : IProductService
    {
        private readonly AppDBContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public ProductRepository(AppDBContext dbContext, IConfiguration configuration, IMapper mapper)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _mapper = mapper;
        }
      
        public async Task<Product> CreateProductAsync(SendProductDTO productDTO)
        {
            if (productDTO == null)
                throw new ArgumentNullException(nameof(productDTO), "Product data cannot be null.");
            var categoryExists = await _dbContext.Categories.AnyAsync(c => c.Id == productDTO.CategoryId);
            if (!categoryExists)
            {
                throw new ArgumentException("Invalid CategoryId. The specified category does not exist.");
            }


            var product = _mapper.Map<Product>(productDTO);

            product.Image = Files.UploadFile(productDTO.Image, "Product");
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }


        public async Task<bool> DeleteProductAsync(int id)
        {
            var pro = await _dbContext.Products.FindAsync(id);
           
            if (pro != null)
            {
                _dbContext.Products.Remove(pro);
                await _dbContext.SaveChangesAsync();
                Files.DeleteFile(pro.Image, "Category");
              return true;
            }
            return false;
           

        }

        public async Task<List<GetProductDTO>> GetAllProductsAsync()
        {
            var products = await _dbContext.Products.Include(c => c.Category).ToListAsync();

            if (products == null || products.Count == 0)
            {
                return null;
            }

            var productDTOs = _mapper.Map<List<GetProductDTO>>(products);
            for (int i = 0; i < productDTOs.Count(); i++)
            {
                productDTOs[i].ImagePath = $"{_configuration["BaseURL"]}/Images/Product/{productDTOs[i].ImagePath}";

            }
            return productDTOs;
        }

        public async Task<GetProductDTO> GetProductByIdAsync(int id)
        {
            var pro = await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);
          
                var Url = $"{_configuration["BaseURL"]}/Images/Product/{pro.Image}";
                var productDTOs = _mapper.Map<GetProductDTO>(pro);

                productDTOs.ImagePath = Url;

                return productDTOs;
        
        }

        public async Task<List<GetProductDTO>> SearchProducts([FromQuery] string name)
        {
            var query = _dbContext.Products.Include(c => c.Category).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));
            }

            var products = await query.ToListAsync();

            if (!products.Any())
            {
                return new List<GetProductDTO>();
            }

            // Map products to DTOs
            var productDTOs = _mapper.Map<List<GetProductDTO>>(products);

            // Update ImagePath for each product DTO
            foreach (var productDto in productDTOs)
            {
                productDto.ImagePath = $"{_configuration["BaseURL"]}/Images/Product/{productDto.ImagePath}";
            }

            return productDTOs;
        }


        public async Task<bool> UpdateProductAsync( int id, SendProductDTO product)
        {
            var pro = await _dbContext.Products.FindAsync(id);
            if (pro != null)
            {
                var imgname = Files.UploadFile(product.Image, "Product");

                pro.Name = product.Name;
                pro.Image = imgname;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
