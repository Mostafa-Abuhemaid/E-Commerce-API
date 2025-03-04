﻿using AutoMapper;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using E_Commerce.Core.DTO;
using E_Commerce.Core.DTO.AccountDTO;
using E_Commerce.Core.Enums;
using E_Commerce.Core.Identity;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static DotNetOpenAuth.OpenId.Extensions.AttributeExchange.WellKnownAttributes.Contact;

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
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService,
        IEmailService emailService,
        IMemoryCache memoryCache,
        ILogger<AccountRepository> logger,
        IMapper mapper,
        RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _memoryCache = memoryCache;
            _logger = logger;
            _mapper = mapper;
            _roleManager = roleManager;
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
                throw new Exception("Your email is not found.");
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

        public async Task<TokenDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) throw new Exception("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) throw new Exception("Invalid email or password");

            return new TokenDTO
            {
                Email = loginDTO.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
        }

        public async Task<TokenDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (existingUser != null)
                throw new Exception("A user with this email already exists.");

            if (registerDTO.Password != registerDTO.ConfirmPassword)
                throw new Exception("Password and Confirm Password do not match");

            if (!new EmailAddressAttribute().IsValid(registerDTO.Email))
                throw new Exception("Invalid email format.");

              var user = _mapper.Map<ApplicationUser>(registerDTO);
            
            
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
           

            await _userManager.AddToRoleAsync(user, "User");

            if (!result.Succeeded)
                throw new Exception("User creation failed");

            return new TokenDTO
            {
                Email = registerDTO.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO resetPassword)
        {
            if (resetPassword.NewPassword != resetPassword.ConfirmNewPassword)
                throw new Exception("Password and Password confirmation do not match");

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null) throw new Exception("Email is not found!");

            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.NewPassword);
            if (!result.Succeeded) throw new Exception("Password reset failed");

            return true;
        }

        public async Task<bool> VerifyOTPAsync(VerifyCodeDTO verify)
        {
            var user = await _userManager.FindByEmailAsync(verify.Email);
            if (user == null) throw new Exception($"Email '{verify.Email}' is not found.");

            var cachedOtp = _memoryCache.Get(verify.Email)?.ToString();
            if (string.IsNullOrEmpty(cachedOtp)) throw new Exception("OTP not found or has expired.");

            if (verify.CodeOTP != cachedOtp) throw new Exception("Invalid OTP.");

            return true;
        }

    }
}
