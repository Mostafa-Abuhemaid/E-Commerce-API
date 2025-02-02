using E_Commerce.Core.DTO;
using E_Commerce.Core.DTO.AccountDTO;
using E_Commerce.Core.Identity;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Implementation
{
    public class AccountRepository : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<AccountRepository> _logger;

    public AccountRepository(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService,
        IEmailService emailService,
        IMemoryCache memoryCache,
        ILogger<AccountRepository> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _emailService = emailService;
        _memoryCache = memoryCache;
        _logger = logger;
    }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {      
            if (email==null)
            {
                return true;
                
            }
            var user = await _userManager.FindByEmailAsync(email);

            return user != null;
        }

        public async Task<ForgotPasswordDTO> ForgotPasswordAsync(ForgotDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new KeyNotFoundException("Your email is not found.");
            }

            var otp = new Random().Next(100000, 999999).ToString();
            _memoryCache.Set(request.Email, otp, TimeSpan.FromMinutes(60));
            await _emailService.SendEmailAsync(request.Email, "Clothing Store", $"Your VerifyOTP code is: {otp}");

            return new ForgotPasswordDTO
            {
                Token = await _userManager.GeneratePasswordResetTokenAsync(user),
                Message = "Check your mail!"
            };
        }

        public Task<TokenDTO> LoginAsync(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }

        public Task<TokenDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }

        public Task<string> ResetPasswordAsync(ResetPasswordDTO resetPassword)
        {
            throw new NotImplementedException();
        }

        public Task<string> VerifyOTPAsync(VerifyCodeDTO verify)
        {
            throw new NotImplementedException();
        }
    }
}
