using AutoMapper;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.Application.Dtos;

namespace Bcan.Backend.Application.Profiles
{
    public class MediaProfile : Profile
    {
        public MediaProfile()
        {
            CreateMap<MediaResolution, MediaResolutionDto>().ReverseMap();
            CreateMap<Media, MediaDto>().ReverseMap();
        }
    }
}