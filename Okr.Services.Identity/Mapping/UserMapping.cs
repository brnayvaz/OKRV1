using AutoMapper;
using Okr.Entities;
using Okr.Services.Identity.Model;
using System.Linq.Expressions;
using static Okr.Services.Identity.Mapping.UserMapping;

namespace Okr.Services.Identity.Mapping
{
    public class UserMapping : Profile
    {

        public UserMapping()
        {
            CreateMap<User, UserModel>()
                .ForMember(destination => destination.name, operation => operation.MapFrom(source => source.name));
            CreateMap<UserModel, User>()
                .ForMember(destination => destination.name, operation => operation.MapFrom(source => source.name));
            
        }

    }
}
