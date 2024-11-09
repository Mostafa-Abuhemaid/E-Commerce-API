using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.Core.Repository
{
    public interface ICategory
    {

        Task<Category> CreateCategoryAsync(Category category);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);

     

        Task DeleteCategoryAsync(int categoryId);
    }
}
