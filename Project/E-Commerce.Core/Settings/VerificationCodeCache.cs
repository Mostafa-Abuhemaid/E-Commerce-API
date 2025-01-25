using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Settings
{
    public class VerificationCodeCache : IVerificationCodeCache
    {
        private readonly ConcurrentDictionary<string, (string Code, DateTime Expiry)> _codes = new();

        public void RemoveCode(string email)
        {
            _codes.TryRemove(email, out _);
        }

         public void StoreCode(string email, string code)
        {
            _codes[email] = (code, DateTime.UtcNow.AddMinutes(30));
        }

        public bool VerifyCode(string email, string code)
        {
            if (!_codes.TryGetValue(email, out var stored)) return false;
            if (DateTime.UtcNow > stored.Expiry) return false;
            return stored.Code == code;
        }
    }
}