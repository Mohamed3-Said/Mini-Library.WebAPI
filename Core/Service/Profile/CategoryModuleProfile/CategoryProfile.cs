using AutoMapper.Configuration;
using DomainLayer.Models.CategoryModule;
using Shared.DataTransfareObjects.CategoryModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profile.CategoryModuleProfile
{
    public class CategoryProfile : AutoMapper.Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryReadDto>();
        }

    }
}
