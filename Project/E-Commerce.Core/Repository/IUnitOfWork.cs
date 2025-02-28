using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repository
{
    public  interface IUnitOfWork:IDisposable
    {
        IOffersService OffersRepository { get; }
        ICartService CartService { get; }
        IFavoriteService FavoriteService { get; }
        IUserService UserService { get; }
        IProductService ProductService { get; }
        ICategoryService CategoryService { get; }
        IAccountService AccountService { get; }
        IOrderService OrderService { get; }

        Task SaveChangesAsync();
    }
}
