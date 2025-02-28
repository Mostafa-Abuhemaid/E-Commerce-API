using E_Commerce.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTO.OrderDTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public string UserName { get; set; }
        public decimal DeliveryCost { get; set; } = 60;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } 
        public List<OrderItemDTO> OrderItems { get; set; }
    }

}
