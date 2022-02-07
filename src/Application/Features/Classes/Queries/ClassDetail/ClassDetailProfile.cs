using AutoMapper;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.Application.Dtos;

namespace Bcan.Backend.Application.Features.Classes.Queries.ShineClassDetail
{
    public class ShineClassDetailProfile : Profile
    {
        public ShineClassDetailProfile()
        {
            CreateMap<DanceInfo, DanceInfoDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Location, LocationDto>();
            CreateMap<EventPolicy, EventPolicyDto>();
            CreateMap<FeeOption, FeeOptionDto>();
            CreateMap<Fee, FeeDto>();
            CreateMap<MediaResolution, MediaResolutionDto>();
            CreateMap<Media, MediaDto>();
            CreateMap<ShineClass, ShineClassDetailDto>()
                .ForMember(dto => dto.Start, options => options.MapFrom(entity => entity.Time.Start))
                .ForMember(dto => dto.End, options => options.MapFrom(entity => entity.Time.End));
        }
    }
}