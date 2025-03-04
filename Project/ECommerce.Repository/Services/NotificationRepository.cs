using E_Commerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Services
{
    public class NotificationRepository : INotificationService
    {
        public async Task<bool> SendNotificationAsync(string deviceToken, string title, string body)
        {
            try
            {
                //var message = new Message()
               // {
                    //Token = deviceToken,
                    //Notification = new Notification
                    //{
                        //Title = title,
                        //Body = body
                    //}
                //};

                //string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                //Console.WriteLine($"Successfully sent message: {response}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending notification: {ex.Message}");
                return false;
            }
        }

        public Task<bool> SendNotificationToAllAsync(string title, string body)
        {
            throw new NotImplementedException();
        }
    }
}
