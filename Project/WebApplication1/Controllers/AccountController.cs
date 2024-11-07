using E_Commerce.Core.DTO;
using E_Commerce.Core.Identity;
using E_Commerce.Core.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var emailExists = await CheckEmailExists(registerDTO.Email);

            if (emailExists is OkObjectResult resul && (bool)resul.Value)
            {
                return BadRequest("The Email is already in use");
            }
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return BadRequest("Password and Confirm Password donot the same ");
            }

            var user = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                Phone = registerDTO.Phone,
                Location = registerDTO.Location,
                gender = registerDTO.Gender
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            var res = new TokenDTO()
            {
                Email = registerDTO.Email,
              
                Token =await _tokenService.CreateToken(user)
            };
            return Ok(res);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid email or password");

            var res = new TokenDTO()
            {
                Email = loginDto.Email,
              
                Token = await _tokenService.CreateToken(user)
            };
            return Ok(res);
        }
        [HttpGet("CheckEmailExists")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var res= await _userManager.FindByEmailAsync(email) ;
            return Ok(res is not null);
        }
      

    }
}
