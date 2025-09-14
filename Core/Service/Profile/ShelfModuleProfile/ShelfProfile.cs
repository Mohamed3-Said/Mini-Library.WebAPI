using DomainLayer.Models.ShelfModule;
using Shared.DataTransfareObjects.ShelfModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profile.ShelfModuleProfile
{
    public class ShelfProfile : AutoMapper.Profile
    {
        public ShelfProfile()
        {
            CreateMap<ShelfCreateDto, Shelf>();
            CreateMap<ShelfUpdateDto, Shelf>();
            CreateMap<Shelf, ShelfReadDto>();
        }

    }
}
