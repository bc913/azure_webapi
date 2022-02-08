using AutoMapper;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.Application.Dtos;
using Bcan.Backend.SharedKernel;

namespace Bcan.Backend.Application.Features.Classes.Commands.Update
{
    public class UpdateClassProfile : Profile
    {
        public UpdateClassProfile()
        {
            CreateMap<DanceInfoDto, DanceInfo>();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<LocationDto, Location>().ReverseMap();
            CreateMap<EventPolicyDto, EventPolicy>().ReverseMap();
            CreateMap<FeeOptionDto, FeeOption>().ReverseMap();
            CreateMap<FeeDto, Fee>().ReverseMap();
            CreateMap<UpdateClassCommand, ShineClass>()
                .ForMember(entity => entity.Time, options => options.MapFrom(command => new DateTimeOffsetRange(command.Start, command.End)))
                .ForMember(entity => entity.Type, options => options.Ignore())
                .ForMember(entity => entity.Media, options => options.Ignore());
        }
    }
}