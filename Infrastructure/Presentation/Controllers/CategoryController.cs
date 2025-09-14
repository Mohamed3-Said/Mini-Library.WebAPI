using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransfareObjects.CategoryModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<CategoryReadDto>> CreateAsync(CategoryCreateDto createDto)
        {
            var category = await _categoryService.CreateCategoryAsync(createDto);
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryReadDto>> UpdateAsync(int id, CategoryUpdateDto updateDto)
        {
            var category = await _categoryService.UpdateCategoryAsync(id, updateDto);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetByIdAsync(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAllAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
    }
}
