using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions.CategorModuleExceptions;
using DomainLayer.Models.CategoryModule;
using Microsoft.EntityFrameworkCore.Metadata;
using ServiceAbstraction;
using Shared.DataTransfareObjects.CategoryModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService(IUnitOfWork _unitOfWork , IMapper _mapper) : ICategoryService
    {

        public async Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<CategoryReadDto> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
                throw new CategoryNotFoundException(id);

            _mapper.Map(categoryUpdateDto, category);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
                return false;

            await _unitOfWork.CategoryRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
                throw new CategoryNotFoundException(id);

            return _mapper.Map<CategoryReadDto>(category);
        }
    }
}
