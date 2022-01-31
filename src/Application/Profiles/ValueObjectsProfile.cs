using AutoMapper;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.Application.Dtos;

namespace Bcan.Backend.Application.Profiles
{
    public class FeeProfile : Profile
    {
        public FeeProfile()
        {
            CreateMap<FeeOption, FeeOptionDto>().ReverseMap();
            // .ForMember(dto => dto.Value, options => options.MapFrom(fovo => fovo.Value))
            // .ForMember(dto => dto.Individual, options => options.MapFrom(fovo => fovo.Individual))
            // .ForMember(dto => dto.Payment, options => options.MapFrom(fovo => fovo.Payment))
            // .ForMember(dto => dto.Description, options => options.MapFrom(fovo => fovo.Description))
            // .ReverseMap();
            CreateMap<Fee, FeeDto>().ReverseMap();
        }
    }

    public class MediaResolutionProfile : Profile
    {
        public MediaResolutionProfile()
        {
            CreateMap<MediaResolution, MediaResolutionDto>().ReverseMap();
        }
    }

    public class DanceInfoProfile : Profile
    {
        public DanceInfoProfile()
        {
            // // Enum to/from Enumeration mappings
            // CreateMap<DanceLevel, DanceLevelDto>()
            //     .ConvertUsing(dl => DanceLevel.FromValue<DanceLevelDto>(dl.Id));

            // CreateMap<DanceLevelDto, DanceLevel>()
            //     .ConvertUsing(dto => DanceLevel.FromValue<DanceLevel>(dto.Id));

            // CreateMap<DanceType, DanceTypeDto>()
            //     .ConvertUsing(dt => DanceType.FromValue<DanceTypeDto>(dt.Id));

            // CreateMap<DanceTypeDto, DanceType>()
            //     .ConvertUsing(dto => DanceType.FromValue<DanceType>(dto.Id));

            CreateMap<DanceInfo, DanceInfoDto>().ReverseMap();
        }
    }

    public class EventPolicyProfile : Profile
    {
        public EventPolicyProfile()
        {
            CreateMap<EventPolicy, EventPolicyDto>().ReverseMap();
        }
    }

    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }

    public class ValueObjectsProfile : Profile
    {
        public ValueObjectsProfile()
        {
        }
    }
}