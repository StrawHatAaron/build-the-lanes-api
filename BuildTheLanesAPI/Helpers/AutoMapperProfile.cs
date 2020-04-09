using AutoMapper;
using BuildTheLanesAPI.Entities;
using BuildTheLanesAPI.Models;

namespace BuildTheLanesAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateMap<User, UserModel>();
            // CreateMap<RegisterModel, User>();
            // CreateMap<UpdateModel, User>();

            CreateMap<Users, UserModel>();
            CreateMap<RegisterModel, Users>();
            CreateMap<UpdateModel, Users>();
        }
    }
}
