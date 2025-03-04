using E_Commerce.Core.DTO.CartDTO;
using E_Commerce.Core.DTO.CartItemDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Enums;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repository
{
    public interface ICartService
    {
        Task<CartDTO> GetCartAsync(string userId);
      Task AddToCartAsync(SendCartItemDTO sendCartItemDTO, string userId);

        Task RemoveFromCartAsync(int productId, string userId);
        Task <QuantityDTO> IncrementItemQuantityAsync(int productId, string userId);
        Task<QuantityDTO> DecrementItemQuantityAsync(int productId, string userId);
        
    }
}
