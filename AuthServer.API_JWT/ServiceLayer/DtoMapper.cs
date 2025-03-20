using AutoMapper;
using CoreLayer.DTOs;

namespace ServiceLayer
{
    class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ProductDto, CoreLayer.Models.ProductModel>().ReverseMap();
            CreateMap<UserModelDto, CoreLayer.Models.UserAppModel>().ReverseMap();
        }
    }
}
