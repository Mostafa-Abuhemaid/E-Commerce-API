﻿using E_Commerce.Core.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Service
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync( ApplicationUser user, UserManager<ApplicationUser> userManager);
    }
}
