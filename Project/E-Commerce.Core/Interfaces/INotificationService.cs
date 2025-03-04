using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendNotificationAsync(string deviceToken, string title, string body);
        Task<bool> SendNotificationToAllAsync(string title, string body);
    }
}
