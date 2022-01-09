using AutoMapper;
using Bcan.Backend.Core.Entities;
using System;

namespace Bcan.Backend.Application.Features.Events.Queries.GetEvents
{
    public class ShineEventLiteProfile : Profile
    {
        public ShineEventLiteProfile()
        {
            CreateMap<ShineEvent, ShineEventLiteDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dto => dto.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dto => dto.Type, options => options.MapFrom(src => src.Type.ToString()));
        }
    }
}