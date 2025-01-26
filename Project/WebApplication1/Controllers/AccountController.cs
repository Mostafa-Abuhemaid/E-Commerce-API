using E_Commerce.Core.DTO;
using E_Commerce.Core.DTO.AccountDTO;
using E_Commerce.Core.Identity;
using E_Commerce.Core.Service;
using E_Commerce.Core.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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
        private readonly IEmailService _emailService;
        private readonly IMemoryCache memo;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ITokenService tokenService,
            IEmailService emailService,

            IMemoryCache memo,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenService = tokenService;
            _emailService = emailService;

            this.memo = memo;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var emailExists = await CheckEmailExistsAsync(registerDTO.Email);

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
        public async Task<IActionResult> CheckEmailExistsAsync([FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email cannot be null or empty.");
            }
            var res= await _userManager.FindByEmailAsync(email) ;

            return Ok(res is not null);
        }
        [HttpPost("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword([FromBody] ForgotDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return NotFound("Your email is not found.");

            var otp = new Random().Next(100000, 999999).ToString();
            memo.Set(request.Email, otp, TimeSpan.FromMinutes(60));
            await _emailService.SendEmailAsync(request.Email, "Clothing store", $"Your VerifyOTP code is :{ otp}" );
            return Ok(new ForgotPasswordDTO
            {
                Token = await _userManager.GeneratePasswordResetTokenAsync(user),
                Message = "Check your mail!"
            });

        }
        [HttpPost("VerifyOTP")]
        public async Task<ActionResult> VerifyOTP([FromBody] VerifyCodeDTO verify)
        {
         
            var user = await _userManager.FindByEmailAsync(verify.Email);
            if (user == null)
            {
                return NotFound($"Email '{verify.Email}' is not found.");
            }

            var cachedOtp = memo.Get(verify.Email)?.ToString();
            if (string.IsNullOrEmpty(cachedOtp))
            {
                _logger.LogWarning($"No OTP found for email: {verify.Email}");
                return BadRequest("OTP not found or has expired.");
            }

            if (verify.CodeOTP != cachedOtp)
            {
                _logger.LogWarning($"Invalid OTP for email: {verify.Email}. Expected: {cachedOtp}, Received: {verify.CodeOTP}");
                return BadRequest("Invalid OTP.");
            }

            
            _logger.LogInformation($"OTP verified successfully for email: {verify.Email}");
            return Ok("OTP verified successfully.");
        }
        [HttpPut("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDTO resetPassword)
        {
            if (resetPassword.NewPassword != resetPassword.ConfirmNewPassword)
            {
                return BadRequest("Password and Password confirmation are not matched");
            }

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                return NotFound("Email is not found!");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.ToList());
            }

            return Ok("Password updated successfully.");
        }


      
    }
}
