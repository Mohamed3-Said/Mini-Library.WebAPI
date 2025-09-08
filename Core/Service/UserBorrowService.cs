using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions.BookExceptions;
using DomainLayer.Models.BookModule;
using ServiceAbstraction;
using Shared.DataTransfareObjects.UserBorrowModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserBorrowService(IUnitOfWork _unitOfWork , IMapper _mapper) : IUserBorrowService
    {
        public async Task<UserBorrowToReadDto> CreateBorrowAsync(CreateUserBorrowDto createuserBorrow)
        {
            var userborrow = _mapper.Map<CreateUserBorrowDto,UserBorrow>(createuserBorrow);
             await  _unitOfWork.UserBorrowRepository.AddAsync(userborrow);
             await _unitOfWork.SaveChangesAsync();

            //Return Create => include(username) , (book Tittle)
            var created = await _unitOfWork.UserBorrowRepository.GetByIdAsync(userborrow.UserBorrowId);
            return _mapper.Map<UserBorrow,UserBorrowToReadDto>(created);
        }

        public async Task<UserBorrowToReadDto> UpdateBorrowAsync(UpdateUserBorrowDto updateuserBorrow)
        {
            var userborrow = await _unitOfWork.UserBorrowRepository.GetByIdAsync(updateuserBorrow.UserBorrowId); 
            if(userborrow is not  null)
            {
                _mapper.Map<UpdateUserBorrowDto, UserBorrow>(updateuserBorrow,userborrow);
                // Important: explicitly update BookId
                userborrow.BookId = updateuserBorrow.BookId;
                // clear navigation property so EF rebinds it
                userborrow.Book = null!;
                await _unitOfWork.UserBorrowRepository.UpdateAsync(userborrow);
                await _unitOfWork.SaveChangesAsync();

                //Return Create => include(username) , (book Tittle)
                var created = await _unitOfWork.UserBorrowRepository.GetByIdAsync(userborrow.UserBorrowId);
                return _mapper.Map<UserBorrow,UserBorrowToReadDto>(created);
            }
            throw new UserBorrowNotFoundException(updateuserBorrow.UserBorrowId);
        }

        public async Task<bool> DeleteBorrowAsync(int userBorrowid)
        {
            var userborrow = await _unitOfWork.UserBorrowRepository.GetByIdAsync(userBorrowid);
            if (userborrow is not null)
            {
                await _unitOfWork.UserBorrowRepository.DeleteAsync(userBorrowid);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<UserBorrowToReadDto>> GetAllBorrowsAsync()
        {
           var userborrows = await _unitOfWork.UserBorrowRepository.GetAllAsync();
           return _mapper.Map<IEnumerable<UserBorrow>,IEnumerable<UserBorrowToReadDto>>(userborrows);

        }

        public async Task<UserBorrowToReadDto> GetBorrowByIdAsync(int userBorrowid)
        {
            var userborrow = await _unitOfWork.UserBorrowRepository.GetByIdAsync(userBorrowid);
            if (userborrow is not null)
            {
              return  _mapper.Map<UserBorrow,UserBorrowToReadDto>(userborrow);
            }
            throw new UserBorrowNotFoundException(userBorrowid);
        }

        public async Task<IEnumerable<UserBorrowToReadDto>> GetBorrowsByBookIdAsync(int bookId)
        {
            var userborrow = await _unitOfWork.UserBorrowRepository.GetBorrowsByBookId(bookId);
            return _mapper.Map<IEnumerable<UserBorrow>, IEnumerable<UserBorrowToReadDto>>(userborrow);
        }

        public async Task<IEnumerable<UserBorrowToReadDto>> GetBorrowsByUserSSNAsync(string userSSN)
        {
            var userborrow = await _unitOfWork.UserBorrowRepository.GetBorrowsByUserSSN(userSSN);
            return _mapper.Map<IEnumerable<UserBorrow>, IEnumerable<UserBorrowToReadDto>>(userborrow);
        }

    }
}
