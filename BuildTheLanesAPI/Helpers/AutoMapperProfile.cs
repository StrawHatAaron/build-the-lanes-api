using AutoMapper;
using BuildTheLanesAPI.Models;

namespace BuildTheLanesAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UserModel>();
            CreateMap<RegisterModel, Users>();
            CreateMap<UpdateModel, Users>();
        }
    }
}
