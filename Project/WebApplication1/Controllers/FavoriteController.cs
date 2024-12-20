﻿using E_Commerce.Core.DTO.FavoriteDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IConfiguration _configuration;

        public FavoriteController(IFavoriteService favoriteService, IConfiguration configuration)
        {
            _favoriteService = favoriteService;
            _configuration = configuration;
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddFavorite([FromRoute] int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            await _favoriteService.AddToFavorite(userId, productId);
            return Ok("Product added to favorites.");
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFavorite([FromRoute] int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            try
            {
                await _favoriteService.RemoveFromFavorite(userId, productId);
                return Ok(new { message = "Product removed from favorites." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Favorite not found." });
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var favorites = await _favoriteService.GetFavorites(userId);
            var FavDTO = favorites.Select(f => new FavoriteDTO

            {
                Name = f.Name,
                Price = f.Price,
                ImagePath = f.Image,
                SubCategory = f.SubCategory,
                CategoryId = f.CategoryId
            }
            ).ToList();

          for(int i=0;i< FavDTO.Count();i++)
            {
                FavDTO[i].ImagePath = $"{_configuration["BaseURL"]}/Images/Product/{FavDTO[i].ImagePath}";
            }

            return Ok(FavDTO);
        }
    }

}
