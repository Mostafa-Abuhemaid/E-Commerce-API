
using AutoMapper;
using E_Commerce.Core.DTO;
using E_Commerce.Core.Identity;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Implementation
{
    public class UserRepository : IUserService

    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<bool> EditUserAsync(string userId, [FromBody] UserDTO model)
        {
        
            if (string.IsNullOrEmpty(userId) || model == null)
            {
                return false;
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.Name = model.Name;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.Location = model.Location;
            user.gender = model.Gender;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<UserDTO> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            var userDTO =_mapper.Map<UserDTO>(user);
            return userDTO;
        }
    }
}
