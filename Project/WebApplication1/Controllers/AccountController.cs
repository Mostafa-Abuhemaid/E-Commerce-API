using AutoMapper;
using E_Commerce.Core.DTO;
using E_Commerce.Core.DTO.AccountDTO;
using E_Commerce.Core.Identity;
using E_Commerce.Core.Repository;
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
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ITokenService tokenService,
            IEmailService emailService,

            IMemoryCache memo,
            ILogger<AccountController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenService = tokenService;
            _emailService = emailService;

            this.memo = memo;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            var user = _mapper.Map<ApplicationUser>(registerDTO);

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
            try
            {
                var result = await _unitOfWork.AccountService.LoginAsync(loginDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        [HttpGet("CheckEmailExists")]
        public async Task<IActionResult> CheckEmailExistsAsync( string email)
        {
            var res= await _unitOfWork.AccountService.CheckEmailExistsAsync(email);
            if(res)
                  return BadRequest("The Email is already in use");
            return Ok("The Email is not used");
      
        }
        [HttpPost("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword([FromBody] ForgotDTO request)
        {
            try
            {
                var result = await _unitOfWork.AccountService.ForgotPasswordAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpPost("VerifyOTP")]
        public async Task<ActionResult> VerifyOTP([FromBody] VerifyCodeDTO verify)
        {

            try
            {
                var result = await _unitOfWork.AccountService.VerifyOTPAsync(verify);
                return result ? Ok("OTP verified successfully.") : BadRequest("Invalid OTP.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDTO resetPassword)
        {
            try
            {
                var result = await _unitOfWork.AccountService.ResetPasswordAsync(resetPassword);
                return result ? Ok("Password updated successfully.") : BadRequest("Failed to update password.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


      
    
}
