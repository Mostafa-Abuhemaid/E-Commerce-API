using E_Commerce.Core.DTO.NotificationDTO;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("SendAll")]
        public async Task<IActionResult> SendNotificationToAll([FromBody] NotificationToAllDTO notification)
        {
            var result = await _unitOfWork.NotificationService.SendNotificationToAllAsync(notification.Title, notification.Body);

            if (result)
                return Ok("Notification sent to all users successfully");

            return BadRequest("Failed to send notification");
        }
        [HttpPost("Send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationDTO notification)
        {
            var result = await _unitOfWork.NotificationService.SendNotificationAsync(notification.DeviceToken, notification.Title, notification.Body);

            if (result)
                return Ok("Notification sent successfully");

            return BadRequest("Failed to send notification");
        }
    }
}
