using E_Commerce.Core.Entities;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using Microsoft.EntityFrameworkCore;
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
         public CategoryRepository( AppDBContext context)
        {
          
            _Context = context;
        }


        public async Task<Category> CreateCategoryAsync(Category category)
        {
             _Context.Categories.Add(category);
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
        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _Context.Products
                                 .Where(p => p.CategoryId == categoryId)
                                 .ToListAsync();
        }

        
    }
}
