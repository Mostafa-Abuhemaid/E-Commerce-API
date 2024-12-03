using E_Commerce.Core.DTO.ProductDTO;
using E_Commerce.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.Core.Repository
{
    public interface IProduct
    {

        Task<GetProductDTO> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(SendProductDTO product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);

    }
}
