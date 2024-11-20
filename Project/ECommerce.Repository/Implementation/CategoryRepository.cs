using AutoMapper;
using E_Commerce.Core.DTO.CategoryDTO;
using E_Commerce.Core.DTO.ProductDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using ECommerce.Repository.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Implementation
{
    public class CategoryRepository : ICategory
    {
        private readonly AppDBContext _Context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CategoryRepository(AppDBContext context, IMapper mapper, IConfiguration configuration)
        {

            _Context = context;
            _mapper = mapper;
            _configuration = configuration;
        }


        public async Task<Category> CreateCategoryAsync(SendCategoryDTO categoryDTO)
        {

            var imgName = Files.UploadFile(categoryDTO.formFile, "Category");
            if (string.IsNullOrEmpty(imgName))
            {
                throw new Exception("File upload failed.");
            }

            var category = new Category
            {
                Name = categoryDTO.Name,
                ImgeURL = imgName
            };

            await _Context.Categories.AddAsync(category);
            await _Context.SaveChangesAsync();

            return category;


        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _Context.Categories.FindAsync(id);

            if (category != null)
            {
                _Context.Categories.Remove(category);
                await _Context.SaveChangesAsync();
              
            }

        }
        public async Task<List<GetProductDTO>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await _Context.Products
                                 .Where(p => p.CategoryId == categoryId)
                                 .Include(c => c.Category)
                                 .ToListAsync();
           
            var productDTOs = _mapper.Map<List<GetProductDTO>>(products);

            for (int i = 0; i < productDTOs.Count(); i++)
            {
                productDTOs[i].ImagePath = $"{_configuration["BaseURL"]}/Images/Product/{productDTOs[i].ImagePath}";

            }
            return productDTOs;
        }

        public async Task<List<GetCategoryDTO>> GetAllCategory()
        {
            var categories = await _Context.Categories.ToListAsync();
         var cat=    _mapper.Map<List<GetCategoryDTO>>(categories);
            for (int i = 0; i < cat.Count(); i++)
            {
                cat[i].imgURL = $"{_configuration["BaseURL"]}/Images/Category/{cat[i].imgURL}";
            }
            return cat;
        }
    }

}
