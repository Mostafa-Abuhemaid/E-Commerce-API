using AutoMapper;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using E_Commerce.Core.DTO;
using E_Commerce.Core.DTO.AccountDTO;
using E_Commerce.Core.DTO.CategoryDTO;
using E_Commerce.Core.DTO.FavoriteDTO;
using E_Commerce.Core.DTO.OrderDTO;
using E_Commerce.Core.DTO.ProductDTO;
using E_Commerce.Core.DTO.UserDto;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Identity;
using Microsoft.AspNetCore.Identity;
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

            CreateMap<RegisterDTO, ApplicationUser>()
                  .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
                    //  .ForMember(dest => dest.gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender.ToString()))); ;

            CreateMap<ApplicationUser, UserDTO>();

            CreateMap<ApplicationUser, UserDto>()
           .ForMember(dest => dest.Role, opt => opt.Ignore());

            ////Order
            CreateMap<Order, OrderDTO>()
          .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
              .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.DateTime))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status.ToString()))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserApp.Name));
            

            CreateMap<CartItem, OrderItem>()  
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
