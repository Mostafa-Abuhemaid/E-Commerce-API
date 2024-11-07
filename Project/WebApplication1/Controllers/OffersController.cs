using E_Commerce.Core.DTO;
using E_Commerce.Core.Entities;
using ECommerce.Repository.Data;
using ECommerce.Repository.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly AppDBContext _appContext;
        private readonly IConfiguration _configuration;
        public OffersController(AppDBContext appContext, IConfiguration configuration)
        {
            _appContext = appContext;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOferrAsync([FromForm] GetOffersDTO offersDTO)
        {
            if (ModelState.IsValid)
            {
                var imgname = Files.UploadFile(offersDTO.Image, "Offers");
                Special_Offers special_Offers = new Special_Offers
                {
                    ImgURL = imgname
                };
                await _appContext.AddAsync(special_Offers);
                await _appContext.SaveChangesAsync();
                return Ok("offer has be added");
            }
            else
                return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferAsync([FromRoute] int id)
        {
            var offer = await _appContext.Special_Offers.FirstOrDefaultAsync(x => x.Id == id);
            if (ModelState.IsValid)
            {
                var Url = $"{_configuration["BaseURL"]}/Images/Offers/{offer.ImgURL}";
                var off = new SendOffer
                {
                    Image = Url
                };
                return Ok(off);
            }
            else
                return BadRequest();
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllOffersAsync()
        {
            var offers = await _appContext.Special_Offers.ToListAsync();
            if (offers == null)
            {
                return NotFound();
            }

            return Ok(offers);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer([FromRoute]int id)
        {
            var offer =await _appContext.Special_Offers.FirstOrDefaultAsync(x => x.Id == id);
            if (offer is not null)
            {
                _appContext.Special_Offers.Remove(offer);
                await _appContext.SaveChangesAsync();
                return Ok($"{offer.Id} has deleted succesfuly");

            }
            else return BadRequest();

        }

    }
}
