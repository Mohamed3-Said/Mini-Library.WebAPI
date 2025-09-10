using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions.AuthorExceptions;
using DomainLayer.Models.AuthorModule;
using ServiceAbstraction;
using Shared.DataTransfareObjects.AuthorModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthorService(IUnitOfWork _unitOfWork, IMapper _mapper) : IAuthorService
    {
        public async Task<AuthorToReadDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto)
        {
            var author = _mapper.Map<CreateAuthorDto, Author>(createAuthorDto);
            await _unitOfWork.AuthorRepository.AddAsync(author);
            // After UnitOfWork SaveChanges:
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Author, AuthorToReadDto>(author);
        }
        public async Task<AuthorToReadDto> UpdateAuthorAsync(int id, UpdateAuthorDto updateAuthorDto)
        {
            var author = await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(id);
            if (author is not null)
            {
                _mapper.Map<UpdateAuthorDto,Author>(updateAuthorDto,author);
                await _unitOfWork.AuthorRepository.UpdateAsync(author);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<Author, AuthorToReadDto>(author);
            }
            throw new AuthorNotFoundException(id);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(id);
            if(author is not null)
            {
                await _unitOfWork.AuthorRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<AuthorToReadDto>> GetAllAuthorsAsync()
        {
            var authors = await _unitOfWork.AuthorRepository.GetAllAuthorsAsync();
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorToReadDto>>(authors);
        }

        public async Task<AuthorToReadDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(id);
            if(author is not null)
            {
               return  _mapper.Map<Author,AuthorToReadDto>(author);
            }
            throw new AuthorNotFoundException(id);
        }

    }
}
