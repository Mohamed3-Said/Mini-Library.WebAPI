using Shared.DataTransfareObjects.AuthorModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorToReadDto>> GetAllAuthorsAsync();
        Task<AuthorToReadDto?> GetAuthorByIdAsync(int id);
        Task<AuthorToReadDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto);
        Task<AuthorToReadDto> UpdateAuthorAsync(int id , UpdateAuthorDto updateAuthorDto);
        Task<bool> DeleteAuthorAsync(int id);

    }
}
