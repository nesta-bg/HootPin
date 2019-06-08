using AutoMapper;
using HootPin.Core.Dtos;
using HootPin.Core.Models;

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