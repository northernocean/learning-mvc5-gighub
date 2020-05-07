using AutoMapper;
using GigHub.Models;
using GigHub.Models.Dtos;

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