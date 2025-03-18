using AutoMapper;
using BackEndApi.Models;

namespace BackEndApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DTO.Request.Product_Create, Product>();

        CreateMap<DTO.Request.Categories_Create, Categories>();
    }
}