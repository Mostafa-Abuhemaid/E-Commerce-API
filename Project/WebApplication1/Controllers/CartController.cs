using E_Commerce.Core.DTO;
using E_Commerce.Core.Enums;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
          [Authorize]
        [HttpGet("get")]
        public async Task<IActionResult> GetCart()
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User not authenticated.");
            }

            var cart = await _cartService.GetCartAsync(userId);
            return cart != null ? Ok(cart) : NotFound("Cart not found.");
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] SendCartItemDTO SendCartItemDTO,Size s)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.AddToCartAsync(SendCartItemDTO, userId,s);
            return Ok("done");
        }

        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.RemoveFromCartAsync(productId, userId);
            return Ok();
        }

        [HttpPost("increment/{productId}")]
        public async Task<IActionResult> IncrementItemQuantity(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.IncrementItemQuantityAsync(productId, userId);
            return Ok();
        }

        [HttpPost("decrement/{productId}")]
        public async Task<IActionResult> DecrementItemQuantity(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.DecrementItemQuantityAsync(productId, userId);
            return Ok();
        }
    }
}
