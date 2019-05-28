using AutoMapper;
using HootPin.Dtos;
using HootPin.Models;

namespace HootPin.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Hoot, HootDto>();
            Mapper.CreateMap<Notification, NotificationDto>();
        }
    }
}