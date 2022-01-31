using AutoMapper;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.Application.Dtos;

namespace Bcan.Backend.Application.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Location, LocationLiteDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dto => dto.City, options => options.MapFrom(src => src.Address.City));
        }
    }
}