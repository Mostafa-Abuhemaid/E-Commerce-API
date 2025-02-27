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
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
          
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                var result = await _unitOfWork.AccountService.RegisterAsync(registerDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
