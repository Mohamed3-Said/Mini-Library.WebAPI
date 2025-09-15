using DomainLayer.Models.BookModule;
using Shared.DataTransfareObjects.UserBorrowModuleDto;
using Shared.DataTransfareObjects.UserModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profile.BookModuleProfile
{
    public class UserBorrowProfile : AutoMapper.Profile
    {
        public UserBorrowProfile()
        {
            CreateMap<CreateUserBorrowDto, UserBorrow>();
            CreateMap<UpdateUserBorrowDto, UserBorrow>()
                 .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId));
            CreateMap<UserBorrow, UserBorrowToReadDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.EmployeeFullName,
               opt => opt.MapFrom(src => src.Employee != null ?
                                          src.Employee.FName + " " + src.Employee.LName
                                         : string.Empty)).ReverseMap();

        }
    }
}
