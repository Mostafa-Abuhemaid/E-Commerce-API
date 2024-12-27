using E_Commerce.Core.DTO;
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
        Task<bool> EditUserAsync(string userId,[FromBody] UserDTO model);
    }
}
