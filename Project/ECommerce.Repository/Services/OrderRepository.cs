using AutoMapper;
using E_Commerce.Core.DTO.OrderDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Enums;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Implementation
{
    public class OrderRepository : IOrderService
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        public OrderRepository(AppDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<OrderDTO> CreateOrderAsync(string userId)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserAppId == userId);

            if (cart == null || !cart.Items.Any())
                throw new Exception("Cart is empty");


            var order = new Order
            {
                UserAppId = userId,
                DateTime = DateTime.UtcNow,
                status = OrderStatus.Pending,
                subtotal = cart.TotalAmount,
                  
                OrderItems = _mapper.Map<List<OrderItem>>(cart.Items) 
            };
            var orderDto=   _mapper.Map<OrderDTO>(order);

            _dbContext.Orders.Add(order);
            _dbContext.CartItems.RemoveRange(cart.Items); 
            await _dbContext.SaveChangesAsync();
            return orderDto;
        }

        public async Task<bool> DeleteOrderByIdAsync(int id)
        {
           var order=  _dbContext.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                throw new Exception($"No Order with id {id}");
             _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _dbContext.Orders 
                .Include(o => o.UserApp)
                 .Include(o => o.OrderItems)
                 .ThenInclude(oi => oi.Product)
                 .ToListAsync();

            var orderDtos = _mapper.Map<List<OrderDTO>>(orders);
            if (!orderDtos.Any())
                throw new Exception("No Order Found");

            return orderDtos;
            


        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
        
                var order = await _dbContext.Orders
                    .Include(o => o.UserApp) 
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new Exception("No order Found");
                var orderDto = _mapper.Map<OrderDTO>(order);
            return orderDto;
            }

        
    }
}
