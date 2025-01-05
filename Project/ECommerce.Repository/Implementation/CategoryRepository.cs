using AutoMapper;
using E_Commerce.Core.DTO.CategoryDTO;
using E_Commerce.Core.DTO.ProductDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using ECommerce.Repository.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Implementation
{
    public class CategoryRepository : ICategoryService
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

            var imgName = Files.UploadFile(categoryDTO.Image, "Category");
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

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _Context.Categories.FindAsync(id);

            if (category != null)
            {
                _Context.Categories.Remove(category);
                await _Context.SaveChangesAsync();

                Files.DeleteFile(category.ImgeURL, "Category");
                return true;
            }
            return false;

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
            var cat = _mapper.Map<List<GetCategoryDTO>>(categories);
            for (int i = 0; i < cat.Count(); i++)
            {
                cat[i].imgURL = $"{_configuration["BaseURL"]}/Images/Category/{cat[i].imgURL}";
            }
            return cat;
        }

        public async Task<bool> UpdateCategoryAsync([FromRoute] int id, SendCategoryDTO sendCategoryDTO)
        {
            var Cat = await _Context.Categories.FindAsync(id);
            if (Cat != null)
            {
                var imgname = Files.UploadFile(sendCategoryDTO.Image, "Category");

                Cat.Name = sendCategoryDTO.Name;
                Cat.ImgeURL = imgname;

                await _Context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

}
