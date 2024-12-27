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
    public interface IProductService
    {

        Task<GetProductDTO> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(SendProductDTO product);
        Task <bool>UpdateProductAsync([FromRoute] int id, SendProductDTO product);
        Task <bool>DeleteProductAsync(int id);
        Task<List<GetProductDTO>> GetAllProductsAsync();
        Task<List<GetProductDTO>> SearchProducts([FromQuery] string name);
    }
}
