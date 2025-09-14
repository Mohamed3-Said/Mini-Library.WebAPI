using Shared.DataTransfareObjects.CategoryModuleDto;
using Shared.DataTransfareObjects.PublisherModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface ICategoryService
    {
        Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<CategoryReadDto> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto);
        Task<bool> DeleteCategoryAsync(int id);  
        Task<CategoryReadDto?> GetCategoryByIdAsync(int id);
        Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync();

    }
}
