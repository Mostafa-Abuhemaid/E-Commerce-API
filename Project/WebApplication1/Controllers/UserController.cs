using E_Commerce.Core.DTO;
using E_Commerce.Core.Identity;
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

        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        private readonly SignInManager<ApplicationUser> _signInManager;



        [HttpGet("GetUserDetails")]
        public async Task<ActionResult<UserDTO>> GetUserDetails()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userDetails = new UserDTO
            {
              
                Name = user.Name,
                Email=user.Email,
                Phone = user.Phone,
                Location = user.Location,
                Gender = user.gender
            };

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
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.Name = model.Name;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.Location = model.Location;
            user.gender = model.Gender;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User updated successfully.");
        }
    }
}

