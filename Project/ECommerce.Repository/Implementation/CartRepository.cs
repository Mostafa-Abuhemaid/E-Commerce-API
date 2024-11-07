using E_Commerce.Core.DTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Enums;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerce.Repository.Implementation
{
 public class CartRepository : ICartService
    {


        private readonly AppDBContext _context;

        public CartRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<CartDTO> GetCartAsync(string userId)
        {
            var cart = await _context.Carts

        .Include(c => c.Items)
        .ThenInclude(i => i.Product)
        .FirstOrDefaultAsync(c => c.UserAppId == userId);


            var cartDto = new CartDTO
            {
              
                UserAppId = cart.UserAppId,
                Items = cart.Items.Select(item => new GetCartItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    TotalPriceForProduct = item.TotalPriceForProduct
                }).ToList(),
                TotalAmount = cart.TotalAmount
            };
            return cartDto;

        }

        public async Task AddToCartAsync(SendCartItemDTO sendCartItemDTO, string userId, Size size)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserAppId == userId)
                       ?? new Cart { UserAppId = userId };

            if (cart.Id == 0)
            {
                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync(); 
            }

            var existingItem = await _context.CartItems.FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == sendCartItemDTO.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += sendCartItemDTO.Quantity; 
            }
            else
            {
                var cartItem = new CartItem
                {
                    ProductId = sendCartItemDTO.ProductId,
                    Quantity = sendCartItemDTO.Quantity,
                    CartId = cart.Id,
                     size = size
                };
                await _context.CartItems.AddAsync(cartItem); 
            }

            await _context.SaveChangesAsync(); 
        }

        public async Task RemoveFromCartAsync(int productId, string userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserAppId == userId);
            if (cart == null) throw new Exception("Cart not found.");

            var item = await _context.CartItems.FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);
            if (item == null) throw new Exception("Item not found in cart.");

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task IncrementItemQuantityAsync(int productId, string userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserAppId == userId);
            if (cart == null) throw new Exception("Cart not found.");

            var item = await _context.CartItems.FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);
            if (item == null) throw new Exception("Item not found in cart.");

            item.Quantity++;
            await _context.SaveChangesAsync(); 
        }

        public async Task DecrementItemQuantityAsync(int productId, string userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserAppId == userId);
            if (cart == null) throw new Exception("Cart not found.");

            var item = await _context.CartItems.FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);
            if (item == null) throw new Exception("Item not found in cart.");

            if (item.Quantity > 1)
            {
                item.Quantity--; 
            }
            else
            {
                _context.CartItems.Remove(item); 
            }

            await _context.SaveChangesAsync(); 
        }
    }
 
}
  