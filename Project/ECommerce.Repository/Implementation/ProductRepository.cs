using AutoMapper;
using E_Commerce.Core.DTO.ProductDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using ECommerce.Repository.Helper;
using ECommerce.Repository.Migrations;
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

            var product = _mapper.Map<Product>(productDTO);

            product.Image = Files.UploadFile(productDTO.Image, "Product");
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }


        public async Task DeleteProductAsync(int id)
        {
            var pro = await _dbContext.Products.FindAsync(id);
           
            if (pro != null)
            {
                _dbContext.Products.Remove(pro);
                await _dbContext.SaveChangesAsync();
                Files.DeleteFile(pro.Image, "Category");
              

            }
           

        }

        public async Task<GetProductDTO> GetProductByIdAsync(int id)
        {
            var pro = await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);
          
                var Url = $"{_configuration["BaseURL"]}/Images/Product/{pro.Image}";
                var productDTOs = _mapper.Map<GetProductDTO>(pro);

                productDTOs.ImagePath = Url;

                return productDTOs;
        
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
