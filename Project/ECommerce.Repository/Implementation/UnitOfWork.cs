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
        public UnitOfWork(AppDBContext context, IOffersService offersRepository)
        {
            _context = context;
            OffersRepository = offersRepository;
        }
        public IOffersService OffersRepository { get; }

        public ICartService CartService { get; }
        public IFavoriteService FavoriteService { get; }

        public IUserService UserService { get; }

        public IProductService ProductService { get; }

        public ICategoryService CategoryService { get; }

        public void Dispose()
        {
          _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
       await  _context.SaveChangesAsync();
        }
    }
}
