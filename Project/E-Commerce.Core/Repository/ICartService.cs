using E_Commerce.Core.DTO;
using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DTO;

namespace E_Commerce.Core.Repository
{
    public interface ICartService
    {
        Task<CartDTO> GetCartAsync(string userId);
        Task AddToCartAsync(SendCartItemDTO sendCartItemDTO, string userId);
        Task RemoveFromCartAsync(int productId, string userId);
        Task IncrementItemQuantityAsync(int productId, string userId);
        Task DecrementItemQuantityAsync(int productId, string userId);
    }
}
