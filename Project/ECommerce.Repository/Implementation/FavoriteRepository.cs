using E_Commerce.Core.Entities;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using ECommerce.Repository.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Implementation
{

    public class FavoriteRepository : IFavoriteService
    {
        private readonly AppDBContext _context;

        public FavoriteRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task AddToFavorite(string userId, int productId)
        {
            var favorite = new Favorite
            {
                UserId = userId,
                ProductId = productId
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromFavorite(string userId, int productId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);

            if (favorite == null)
                throw new KeyNotFoundException("Favorite not found.");

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetFavorites(string userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.Product)
                .ToListAsync();
        }
    }
}
