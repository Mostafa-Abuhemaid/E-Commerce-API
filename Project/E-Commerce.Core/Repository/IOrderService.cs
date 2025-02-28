using E_Commerce.Core.DTO.OrderDTO;
using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repository
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateOrderAsync(string userId);
        Task <List<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO> GetOrderByIdAsync(int id);
        Task<bool> DeleteOrderByIdAsync(int id);
    }
}
