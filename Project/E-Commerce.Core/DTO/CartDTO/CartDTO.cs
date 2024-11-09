using E_Commerce.Core.DTO.CartItemDTO;

namespace E_Commerce.Core.DTO.CartDTO
{
    public class CartDTO
    {

        public string UserAppId { get; set; }
        public List<GetCartItemDto> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
