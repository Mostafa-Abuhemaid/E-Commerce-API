using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTO.FavoriteDTO
{
    public class IsFavorite
    {
        public int ProductId { set; get; }
        public bool Is_Favorite { set; get; }
    }
}
