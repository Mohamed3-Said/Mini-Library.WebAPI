using AutoMapper;
using DomainLayer.Models.UserModule;
using Shared.DataTransfareObjects.UserModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profile.UserModuleProfile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UserToReadDto>();
           // CreateMap<IEnumerable<User>, IEnumerable<UserToReadDto>>();

        }
    }
}
