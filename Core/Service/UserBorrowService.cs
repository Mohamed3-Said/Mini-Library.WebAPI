using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions.BookExceptions;
using DomainLayer.Exceptions.EmployeeExceptions;
using DomainLayer.Exceptions.UserExceptions;
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
    public class UserBorrowService(IUnitOfWork _unitOfWork , IMapper _mapper ,
        IUserRepository _userRepository , 
        IEmployeeRepository _employeeRepository , IBookRepository _bookRepository) : IUserBorrowService
    {
        public async Task<UserBorrowToReadDto> CreateBorrowAsync(CreateUserBorrowDto createuserBorrow)
        {
            var user = await _userRepository.GetUserBySSNAsync(createuserBorrow.UserSSN);
            if(user is null)
                throw new UserNotFoundException(createuserBorrow.UserSSN);

            var book = await  _bookRepository.GetBookByIdAsync(createuserBorrow.BookId);
            if(book is null)
                throw new BookNotFoundedException(createuserBorrow.BookId);

            var employee = await _employeeRepository.GetByIdAsync(createuserBorrow.EmployeeId);
            if(employee is null)
                throw new EmployeeNotFoundException(createuserBorrow.EmployeeId);


            var userborrow = _mapper.Map<CreateUserBorrowDto,UserBorrow>(createuserBorrow);
             await  _unitOfWork.UserBorrowRepository.AddAsync(userborrow);
             await _unitOfWork.SaveChangesAsync();

            //Return Create => include(username) , (book Tittle)
            var created = await _unitOfWork.UserBorrowRepository.GetByIdAsync(userborrow.UserBorrowId);
            return _mapper.Map<UserBorrow,UserBorrowToReadDto>(created!);
        }

        public async Task<UserBorrowToReadDto> UpdateBorrowAsync(UpdateUserBorrowDto updateuserBorrow)
        {
            var userborrow = await _unitOfWork.UserBorrowRepository.GetByIdAsync(updateuserBorrow.UserBorrowId); 
            if(userborrow is not  null)
            {

                var user = await _userRepository.GetUserBySSNAsync(updateuserBorrow.UserSSN);
                if (user is null)
                    throw new UserNotFoundException(updateuserBorrow.UserSSN);

                var book = await _bookRepository.GetBookByIdAsync(updateuserBorrow.BookId);
                if (book is null)
                    throw new BookNotFoundedException(updateuserBorrow.BookId);

                var employee = await _employeeRepository.GetByIdAsync(updateuserBorrow.EmployeeId);
                if (employee is null)
                    throw new EmployeeNotFoundException(updateuserBorrow.EmployeeId);

                // Map the updated fields from DTO to entity
                _mapper.Map<UpdateUserBorrowDto, UserBorrow>(updateuserBorrow,userborrow);
                // Important: explicitly update BookId and EmployeeId
                userborrow.BookId = updateuserBorrow.BookId;
                userborrow.EmployeeId = updateuserBorrow.EmployeeId;    
                // clear navigation property so EF rebinds it
                userborrow.Book = null!;
                userborrow.Employee = null!;

                await _unitOfWork.UserBorrowRepository.UpdateAsync(userborrow);
                await _unitOfWork.SaveChangesAsync();

                //Return Create => include(username) , (book Tittle) , (employee name)
                var created = await _unitOfWork.UserBorrowRepository.GetByIdAsync(userborrow.UserBorrowId);
                return _mapper.Map<UserBorrow,UserBorrowToReadDto>(created!);
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
