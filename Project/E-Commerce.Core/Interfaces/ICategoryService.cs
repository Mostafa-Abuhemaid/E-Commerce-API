using E_Commerce.Core.DTO.CategoryDTO;
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
    public interface ICategoryService
    {

        Task<Category> CreateCategoryAsync(SendCategoryDTO sendCategoryDTO);
        Task<List<GetProductDTO>> GetProductsByCategoryIdAsync(int categoryId);
        Task<bool> DeleteCategoryAsync(int categoryId);
        Task<List<GetCategoryDTO>> GetAllCategory();
        Task<bool> UpdateCategoryAsync([FromRoute] int id, SendCategoryDTO sendCategoryDTO);
    }
}
