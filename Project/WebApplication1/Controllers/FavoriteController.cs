using E_Commerce.Core.DTO.FavoriteDTO;
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
       
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteController(IUnitOfWork unitOfWork)
        {
       
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddFavorite([FromRoute] int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            await _unitOfWork.FavoriteService.AddToFavorite(userId, productId);
            return Ok("Product added to favorites.");
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFavorite([FromRoute] int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            try
            {
                await _unitOfWork.FavoriteService.RemoveFromFavorite(userId, productId);
                return Ok(new { message = "Product removed from favorites." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Favorite not found." });
            }
        }

        [HttpGet("GetAllFavorite")]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var favorites = await _unitOfWork.FavoriteService.GetFavorites(userId);
           if(favorites == null) return Ok("No Product found");
           return Ok(favorites);
        }
        [HttpPost("IsFavorite")]
        public async Task<IActionResult> IsFavorite([FromQuery] int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

           var fav= await _unitOfWork.FavoriteService.IsFavorite(userId, productId);
            return Ok(fav);
        }
    }

}
