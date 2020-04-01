using AutoMapper;
using BuildTheLanesAPI.Entities;
using BuildTheLanesAPI.Models.Users;

namespace BuildTheLanesAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
        }
    }
}
