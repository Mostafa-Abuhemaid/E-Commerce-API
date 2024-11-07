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
    public class ProductRepository : IProduct
    {
        private readonly AppDBContext _dbContext;
        public ProductRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Product> CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products
                                 .Include(p => p.Category) 
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
