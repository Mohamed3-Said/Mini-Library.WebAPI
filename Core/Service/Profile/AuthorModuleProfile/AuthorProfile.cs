using DomainLayer.Models.AuthorModule;
using Shared.DataTransfareObjects.AuthorModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profile.AuthorModuleProfile
{
    public class AuthorProfile : AutoMapper.Profile
    {
        public AuthorProfile()
        {
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
            CreateMap<Author,AuthorToReadDto>().ReverseMap();
        }
    }
}
