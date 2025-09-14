using DomainLayer.Models.BookModule;
using Shared.DataTransfareObjects.BookModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profile.BookModuleProfile
{
    public class BookProfile : AutoMapper.Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
            CreateMap<Book, BookToReadDto>()
            .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Cat_Name))
            .ForMember(dest => dest.ShelfCode, opt => opt.MapFrom(src => src.Shelf.Code))
                .ReverseMap();

            //BookAuthor : => ICollection
        }
    }
}
