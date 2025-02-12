using E_Commerce.Core.DTO.AccountDTO;
using E_Commerce.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repository
{
    public interface IAccountService
    {
        Task<TokenDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<TokenDTO> LoginAsync(LoginDTO loginDTO);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<ForgotPasswordDTO> ForgotPasswordAsync(ForgotDTO request);
        Task<bool> VerifyOTPAsync(VerifyCodeDTO verify);
        Task<bool> ResetPasswordAsync(ResetPasswordDTO resetPassword);
    }
}
