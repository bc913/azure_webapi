using AutoMapper;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.Application.Dtos;
using Bcan.Backend.SharedKernel;

namespace Bcan.Backend.Application.Features.Classes.Commands.Create
{
    public class CreateClassProfile : Profile
    {
        public CreateClassProfile()
        {
            CreateMap<DanceInfo, DanceInfoDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<EventPolicy, EventPolicyDto>().ReverseMap();
            CreateMap<FeeOption, FeeOptionDto>().ReverseMap();
            CreateMap<Fee, FeeDto>().ReverseMap();
            CreateMap<CreateClassCommand, ShineClass>()
                .ForMember(entity => entity.Time, options => options.MapFrom(command => new DateTimeOffsetRange(command.Start, command.End)))
                .ForMember(entity => entity.Type, options => options.Ignore())
                .ForMember(entity => entity.Media, options => options.Ignore());
        }
    }
}