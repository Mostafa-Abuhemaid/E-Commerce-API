using E_Commerce.Core.DTO;

namespace WebApplication1.DTO
{
	public class CartDTO
	{
       
        public string UserAppId { get; set; }
        public List<GetCartItemDto> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
