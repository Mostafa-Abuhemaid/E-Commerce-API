using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Settings
{
    public interface IVerificationCodeCache
    {
        void StoreCode(string email, string code);
        bool VerifyCode(string email, string code);
        void RemoveCode(string email);
    }
}
