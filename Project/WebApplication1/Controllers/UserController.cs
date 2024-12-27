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
    }
}

