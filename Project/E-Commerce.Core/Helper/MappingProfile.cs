using AutoMapper;
using E_Commerce.Core.DTO;
using E_Commerce.Core.DTO.AccountDTO;
using E_Commerce.Core.DTO.CategoryDTO;
using E_Commerce.Core.DTO.FavoriteDTO;
using E_Commerce.Core.DTO.ProductDTO;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            ///Product
            CreateMap<Product, GetProductDTO>().ForMember(p => p.Category, o => o.MapFrom(s => s.Category.Name))
              .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image));

            CreateMap<SendProductDTO, Product>()
               .ForMember(dest => dest.Image, opt => opt.Ignore());
            ///Favorite
            CreateMap<Product, FavoriteDTO>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
            ///Category

            CreateMap<Category, GetCategoryDTO>().ForMember(dest => dest.imgURL, opt => opt.MapFrom(src => src.ImgeURL));
            ///////////////////////////////////////

            CreateMap<RegisterDTO, ApplicationUser>();

            CreateMap<ApplicationUser, UserDTO>();
        }
    }
}
