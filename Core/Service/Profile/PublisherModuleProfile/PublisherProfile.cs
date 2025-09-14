using DomainLayer.Models.PublisherModule;
using Shared.DataTransfareObjects.PublisherModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profile.PublisherModuleProfile
{
    public class PublisherProfile : AutoMapper.Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, PublisherToReadDto>();
            CreateMap<PublisherCreateDto, Publisher>();
            CreateMap<PublisherUpdateDto, Publisher>();
        }

    }
}
