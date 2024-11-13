using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTO.OfferDTO
{
    public class GetOffersDTO
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
    }
}
