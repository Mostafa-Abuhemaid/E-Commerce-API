using E_Commerce.Core.DTO.OfferDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using ECommerce.Repository.Helper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Implementation
{
    public class OfferRepository : IOffersService
    {
        private readonly AppDBContext _dbContext;
        private readonly IConfiguration _configuration;

        public OfferRepository(AppDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public async Task CreateOferrAsync([FromForm] GetOffersDTO offersDTO)
        {
            var imgname = Files.UploadFile(offersDTO.Image, "Offers");
            Special_Offers special_Offers = new Special_Offers
            {
                ImgURL = imgname
            };
            await _dbContext.AddAsync(special_Offers);
            await _dbContext.SaveChangesAsync();
          
        }

        public async Task<bool> DeleteOfferasync([FromRoute] int id)
        {
            var offer = await _dbContext.Special_Offers.FindAsync(id);
            if (offer == null) return false;

            _dbContext.Special_Offers.Remove(offer);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<SendOfferDTO>> GetAllOffersAsync()
        {
            return await _dbContext.Special_Offers
              .Select(x => new SendOfferDTO
              {
                  Id = x.Id,
                  Image = $"{_configuration["BaseURL"]}/Images/Offers/{x.ImgURL}"
              })
              .ToListAsync();
        }

        public async Task<SendOfferDTO> GetOfferAsync([FromRoute] int id)
        {
            var offer = await _dbContext.Special_Offers.FindAsync(id);
            if (offer == null) return null;

            return new SendOfferDTO
            {
                Id = offer.Id,
                Image = $"{_configuration["BaseURL"]}/Images/Offers/{offer.ImgURL}"
            };
        
        }

    
    }
}
