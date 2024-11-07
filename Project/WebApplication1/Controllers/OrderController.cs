using E_Commerce.Core.DTO;
using ECommerce.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDBContext _appContext;
        public OrderController(AppDBContext appContext)
        {
            _appContext = appContext;
        }
        [HttpPost]
        public async Task<IActionResult>GetOrderInfo(OrderDTO orderDTO)
        {
            return Ok(orderDTO);
        }
    }
}
