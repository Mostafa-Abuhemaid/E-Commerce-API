using AutoMapper;
using E_Commerce.Core.DTO.CategoryDTO;
using E_Commerce.Core.DTO.ProductDTO;
using E_Commerce.Core.Entities;
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

            CreateMap<Product, GetProductDTO>().ForMember(p => p.Category, o => o.MapFrom(s => s.Category.Name))
               .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image));

            CreateMap<Category, GetCategoryDTO>().ForMember(dest => dest.imgURL, opt => opt.MapFrom(src => src.ImgeURL));
        }
    }
}
