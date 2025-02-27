using E_Commerce.Core.DTO;
using E_Commerce.Core.Identity;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase

    {

      private readonly IUnitOfWork _unitOfWork;
        public UserController( IUnitOfWork unitOfWork)
        {
        
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetUserDetails")]
        public async Task<ActionResult<UserDTO>> GetUserDetails()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID claim is missing.");
            }

            var userDetails = await _unitOfWork.UserService.GetUserDetailsAsync(userId);

            if (userDetails == null)
            {
                return NotFound();
            }

            return Ok(userDetails);
        }
    

    [HttpPut("EditUser")]
        public async Task<IActionResult> EditUser([FromBody] UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _unitOfWork.UserService.EditUserAsync(userId, model);

            if (!result)
            {
                return BadRequest("Failed to update user.");
            }

            return Ok("User updated successfully.");
        }
       // [Authorize(Roles = "Admin")]
        [HttpPost("lock")]
        public async Task<IActionResult> LockUser(string email)
        {
            var result = await _unitOfWork.UserService.LockUserByEmailAsync(email);
            if (!result) return NotFound("User not found");
            return Ok($"User {email} locked successfully");
        }

       // [Authorize(Roles = "Admin")]
        [HttpPost("unlock")]
        public async Task<IActionResult> UnlockUser(string email)
        {
            var result = await _unitOfWork.UserService.UnlockUserByEmailAsync(email);
            if (!result) return NotFound("User not found");
            return Ok($"User {email} unlocked successfully");
        }
       // [Authorize(Roles = "Admin")] 
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUserByEmail( string email)
        {
            var result = await _unitOfWork.UserService.DeleteUserByEmailAsync(email);

            if (!result) return NotFound(new { message = "User not found" });

            return Ok(new { message = $"User {email} deleted successfully" });
        }
        //[Authorize(Roles = "Admin")] 
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _unitOfWork.UserService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}

