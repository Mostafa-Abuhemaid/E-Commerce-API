using E_Commerce.Core.DTO;
using E_Commerce.Core.DTO.OfferDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Enums;
using E_Commerce.Core.Repository;
using ECommerce.Repository.Data;
using ECommerce.Repository.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
           private readonly IUnitOfWork _unitOfWork;
        public OffersController( IUnitOfWork unitOfWork)
        {
           
         
            
            _unitOfWork = unitOfWork;
        }
       // [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateOferrAsync([FromForm] GetOffersDTO offersDTO)
        {
            if (!ModelState.IsValid || offersDTO.Image == null)
            {
                return BadRequest("Invalid input data.");
            }
            else
            {
             await _unitOfWork.OffersRepository.CreateOferrAsync(offersDTO);
            return Ok("Offer has been added successfully.");
            }
        
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferAsync([FromRoute] int id)
        {
            var offer = await _unitOfWork.OffersRepository.GetOfferAsync(id);
            return Ok(offer);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllOffersAsync()
        {
            var offers = await _unitOfWork.OffersRepository.GetAllOffersAsync();
            if (offers == null)
            {
                return NotFound();
            }

            return Ok(offers);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfferasync([FromRoute]int id)
        {
           var off = _unitOfWork.OffersRepository.DeleteOfferasync(id);
            if (off!=null) return Ok("the offer has been deleted successfuly");
            else return BadRequest();


        }

    }
}
