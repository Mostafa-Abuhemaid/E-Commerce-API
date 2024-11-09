using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTO.FavoriteDTO
{
    public class FavoriteDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? ImagePath { get; set; }

        public string SubCategory { get; set; }

        public int CategoryId { get; set; }
    }
}
