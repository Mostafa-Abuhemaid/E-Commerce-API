using E_Commerce.Core.DTO.CartDTO;
using E_Commerce.Core.DTO.CartItemDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Enums;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerce.Repository.Implementation
{
    public class CartRepository : ICartService
    {


        private readonly AppDBContext _context;
        private readonly IConfiguration _configuration;

        public CartRepository(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
                    SubCategory = item.Product.SubCategory,
                    Material = item.Product.Material,
                    Image = item.Product.Image,
                    TotalPriceForProduct = item.TotalPriceForProduct
                }).ToList(),
                TotalAmount = cart.TotalAmount
            };

            foreach (var item in cartDto.Items)
            {
                item.Image = $"{_configuration["BaseURL"]}/Images/Product/{item.Image}";
            }

            return cartDto;

        }

        public async Task AddToCartAsync(SendCartItemDTO sendCartItemDTO, string userId)
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
                     size = sendCartItemDTO.Size
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

        public async Task<QuantityDTO> IncrementItemQuantityAsync(int productId, string userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserAppId == userId);
            if (cart == null) throw new Exception("Cart not found.");

            var item = await _context.CartItems.FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);
            if (item == null) throw new Exception("Item not found in cart.");

            item.Quantity++;
            var quantit = new QuantityDTO
            {
                ProductId = productId,
                quantity = item.Quantity
            };
            await _context.SaveChangesAsync();
            return quantit;


        }

        public async Task<QuantityDTO> DecrementItemQuantityAsync(int productId, string userId)
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
            var quantit = new QuantityDTO
            {
                ProductId = productId,
                quantity = item.Quantity
            };
            await _context.SaveChangesAsync();
            return quantit;

        }
    }
 
}
  