using E_Commerce.Core.DTO.FavoriteDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using ECommerce.Repository.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public FavoriteRepository(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

        public async Task<List<FavoriteDTO>> GetFavorites(string userId)
        {
           var fav= await _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.Product)
                .ToListAsync();
            var FavDTO = fav.Select(f => new FavoriteDTO


            {
                ProductId=f.Id,
                Name = f.Name,
                Price = f.Price,
                ImagePath = f.Image,
                SubCategory = f.SubCategory,
                CategoryId = f.CategoryId
            }
           ).ToList();

            for (int i = 0; i < FavDTO.Count(); i++)
            {
                FavDTO[i].ImagePath = $"{_configuration["BaseURL"]}/Images/Product/{FavDTO[i].ImagePath}";
            }
            if( FavDTO.Count() > 0 )
            return FavDTO;
            else
            return null;
        }
    }
}
