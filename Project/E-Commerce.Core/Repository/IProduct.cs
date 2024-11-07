using E_Commerce.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DTO;

namespace E_Commerce.Core.Repository
{
    public interface IProduct
    {

        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);

    }
}
