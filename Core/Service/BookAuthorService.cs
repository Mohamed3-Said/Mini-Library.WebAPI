using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions.AuthorExceptions;
using DomainLayer.Exceptions.BookExceptions;
using DomainLayer.Models.AuthorModule;
using DomainLayer.Models.BookModule;
using ServiceAbstraction;
using Shared.DataTransfareObjects.AuthorModuleDto;
using Shared.DataTransfareObjects.BookAuthorModuleDto;
using Shared.DataTransfareObjects.BookModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BookAuthorService(IUnitOfWork _unitOfWork,
        IBookRepository _bookRepository , IMapper _mapper) : IBookAuthorService
    {
        public async Task<int> AssignAuthorsToBookAsync(AssignAuthorsToBookDto dto)
        {
            // 1- Check if Book exists : 
            var book = await _bookRepository.GetBookByIdAsync(dto.BookId);
            if (book == null)
                throw new BookNotFoundedException(dto.BookId);
            // 2- Check if Authors exists :
            foreach (var authorId in dto.AuthorsId)
            {
                var author = await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(authorId);
                if (author == null)
                    throw new AuthorNotFoundException(authorId);
            }
            // 3- Assign Authors to Book :
            await _unitOfWork.BookAuthorRepository.AssignAuthorsToBookAsync(dto.BookId, dto.AuthorsId);
           return await _unitOfWork.SaveChangesAsync();

        }

        public async Task<int> AssignBooksToAuthorAsync(AssignBooksToAuthorDto dto)
        {
            // 1- Check if Author exists :
            var author = await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(dto.AuthorId);
            if (author == null)
                throw new AuthorNotFoundException(dto.AuthorId);

            // 2- Check if Books exists :
            foreach (var bookId in dto.BooksId)
            {
                var book = await _bookRepository.GetBookByIdAsync(bookId);
                if (book == null)
                    throw new BookNotFoundedException(bookId);
            }
            // 3- Assign Books to Author :
            await _unitOfWork.BookAuthorRepository.AssignBooksToAuthorAsync(dto.AuthorId, dto.BooksId);
           return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuthorToReadDto>> GetAllAuthorsByBookIdAsync(int bookId)
        {
            // 1- Check if Book exists : 
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
                throw new BookNotFoundedException(bookId);
            // 2- Get All Authors of this Book :
            var authors = await _unitOfWork.BookAuthorRepository.GetAllAuthorsByBookIdAsync(bookId);
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorToReadDto>>(authors);
        }

        public async Task<IEnumerable<BookToReadDto>> GetAllBooksByAuthorIdAsync(int authorId)
        {
            // 1- Check if Author exists :
            var author = await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(authorId);
            if (author == null)
                throw new AuthorNotFoundException(authorId);
            // 2- Get All Books of this Author :
            var books = await _unitOfWork.BookAuthorRepository.GetAllBooksByAuthorIdAsync(authorId);
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookToReadDto>>(books);
        }
    }
}
