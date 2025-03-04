using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;

        public UnitOfWork(AppDBContext context,
                          IOffersService offersRepository,
                          ICategoryService categoryService,
                          ICartService cartService,
                          IFavoriteService favoriteService,
                          IUserService userService,
                          IProductService productService,
                          IAccountService accountService,
                          IOrderService orderService,
                          INotificationService notificationService)
        {
            _context = context;
            OffersRepository = offersRepository;
            CategoryService = categoryService;
            CartService = cartService;
            FavoriteService = favoriteService;
            UserService = userService;
            ProductService = productService;
            AccountService = accountService;
           OrderService = orderService;
            NotificationService = notificationService;
        }

        public IOffersService OffersRepository { get; }
        public ICategoryService CategoryService { get; }
        public ICartService CartService { get; }
        public IFavoriteService FavoriteService { get; }
        public IUserService UserService { get; }
        public IProductService ProductService { get; }
        public IAccountService AccountService { get; }

        public IOrderService OrderService { get; }

        public INotificationService NotificationService {get;}

    

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
