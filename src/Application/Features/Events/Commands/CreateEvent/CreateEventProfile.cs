using AutoMapper;
using Bcan.Backend.Core.Entities;
using System;

namespace Bcan.Backend.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventProfile : Profile
    {
        public CreateEventProfile()
        {
            /*
            For valid Configuration
            Consolidate the CreateMap calls into one profile, or set the root Advanced.AllowAdditiveTypeMapCreation configuration value to 'true'.
            */
            CreateMap<CreateEventCommand, ShineEvent>()
                .ForMember(entity => entity.Id, options => options.MapFrom(command => Guid.NewGuid()))
                .ForMember(entity => entity.Title, options => options.MapFrom(command => command.Title))
                .ForMember(entity => entity.Type, options => options.MapFrom(command => (ShineEventType)Enum.Parse(typeof(ShineEventType), command.EventType)));
        }
    }
}