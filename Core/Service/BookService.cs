using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions.BookExceptions;
using DomainLayer.Models.BookModule;
using ServiceAbstraction;
using Shared.DataTransfareObjects.BookModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BookService(IBookRepository _bookRepository, IMapper _mapper) : IBookService
    {
        public async Task<BookToReadDto> CreateBookAsync(CreateBookDto bookDto)
        {
            var Book = _mapper.Map<CreateBookDto, Book>(bookDto);
            await _bookRepository.AddAsync(Book);

            var created = await _bookRepository.GetBookByIdAsync(Book.BookId);
            return _mapper.Map<Book, BookToReadDto>(created!);

        }

        public async Task<BookToReadDto> UpdateBookAsync(int id, UpdateBookDto bookDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book is not null)
            {
                _mapper.Map<UpdateBookDto, Book>(bookDto, book);
                await _bookRepository.UpdateAsync(book);

                //Return Update => include (Publisher Name) , (Shelf Name) , (Category Name)
                var updated = await _bookRepository.GetBookByIdAsync(id);
                return _mapper.Map<Book, BookToReadDto>(updated!);
            }
            throw new BookNotFoundedException(id);

        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book is not null)
            {
                await _bookRepository.DeleteAsync(id);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<BookToReadDto>> GetAllBooksAsync()
        {
            var Books = await _bookRepository.GetAllBooksAsync();
            var BooksDto = _mapper.Map<IEnumerable<Book>, IEnumerable<BookToReadDto>>(Books);
            return BooksDto;
        }

        public async Task<BookToReadDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book is not null)
            {
                var bookDto = _mapper.Map<Book, BookToReadDto>(book);
                return bookDto;
            }
            throw new BookNotFoundedException(id);
        }

        public async Task<IEnumerable<BookToReadDto>> SearchBooksAsync(string keyword)
        {
            var books = await _bookRepository.SearchBooksAsync(keyword);
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookToReadDto>>(books);
        }
    }
}
