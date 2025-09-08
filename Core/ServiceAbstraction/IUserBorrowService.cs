using Shared.DataTransfareObjects.UserBorrowModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IUserBorrowService
    {
        Task<UserBorrowToReadDto> CreateBorrowAsync(CreateUserBorrowDto createuserBorrow);
        Task<UserBorrowToReadDto> UpdateBorrowAsync(UpdateUserBorrowDto updateuserBorrow);
        Task<bool> DeleteBorrowAsync(int userBorrowid);
        Task<IEnumerable<UserBorrowToReadDto>> GetAllBorrowsAsync();
        Task<UserBorrowToReadDto> GetBorrowByIdAsync(int userBorrowid);
        Task<IEnumerable<UserBorrowToReadDto>> GetBorrowsByUserSSNAsync(string userSSN);
        Task<IEnumerable<UserBorrowToReadDto>> GetBorrowsByBookIdAsync(int bookId);

    }
}
