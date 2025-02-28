using E_Commerce.Core.DTO.OrderDTO;
using E_Commerce.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpPost("MakeOrder")]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var order = await _unitOfWork.OrderService.CreateOrderAsync(userId);
            return Ok(order);
        }
     //   [Authorize(Roles ="Admin")]
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {

                var orders = await _unitOfWork.OrderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("GetOrder/{id}")]
        public async Task<IActionResult> GetOrderByIdAsnc(int id)
        {
            try
            {
                var order =await _unitOfWork.OrderService.GetOrderByIdAsync(id);
                return Ok(order);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("DeleteOrder/{id}")]
        public async Task <IActionResult> DeleteOrderAsync(int id )
        {
            try
            {
                var order = await _unitOfWork.OrderService.DeleteOrderByIdAsync(id);
                return Ok($"Order {id} deleted successfuly");
            }
            catch( Exception ex )
            {
                return NotFound(ex.Message);
            }
        }

    }
}
