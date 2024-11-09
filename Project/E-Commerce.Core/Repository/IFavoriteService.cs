using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repository
{

    public interface IFavoriteService
    {
        Task AddToFavorite(string userId, int productId);
        Task RemoveFromFavorite(string userId, int productId);
        Task<List<Product>> GetFavorites(string userId);
    }

}
