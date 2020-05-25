using AutoMapper;
using GigHub.Core.Models;
using GigHub.Core.Dtos;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, ArtistDto>();
            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<Notification, NotificationDto>();
        }
    }
}