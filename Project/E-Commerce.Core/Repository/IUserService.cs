using E_Commerce.Core.DTO;
using E_Commerce.Core.DTO.UserDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repository
{
    public interface IUserService
    {
        Task<UserDTO> GetUserDetailsAsync(string userId);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<bool> EditUserAsync(string userId,[FromBody] UserDTO model);
      
        Task<bool> LockUserByEmailAsync(string email);
        Task<bool> UnlockUserByEmailAsync(string email);
        Task<bool> DeleteUserByEmailAsync(string email);
      
    }
}
