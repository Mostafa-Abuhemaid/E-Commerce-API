using E_Commerce.Core.DTO.OfferDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repository
{
    public interface IOffersService
    {
        Task CreateOferrAsync([FromForm] GetOffersDTO offersDTO);
        Task<SendOfferDTO> GetOfferAsync([FromRoute] int id);
        Task <bool>DeleteOfferasync([FromRoute] int id);
        Task<List<SendOfferDTO>> GetAllOffersAsync();

    }
}
