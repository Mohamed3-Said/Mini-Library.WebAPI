using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransfareObjects.ShelfModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShelfController : ControllerBase
    {
        private readonly IShelfService _shelfService;

        public ShelfController(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ShelfReadDto>> CreateAsync(ShelfCreateDto createDto)
        {
            var shelf = await _shelfService.CreateShelfAsync(createDto);
            return Ok(shelf);
        }

        [HttpPut("{code}")]
        public async Task<ActionResult<ShelfReadDto>> UpdateAsync(string code, ShelfUpdateDto updateDto)
        {
            var shelf = await _shelfService.UpdateShelfAsync(code, updateDto);
            return Ok(shelf);
        }

        [HttpDelete("{code}")]
        public async Task<ActionResult<bool>> DeleteAsync(string code)
        {
            var result = await _shelfService.DeleteShelfAsync(code);
            return Ok(result);
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<ShelfReadDto>> GetByIdAsync(string code)
        {
            var shelf = await _shelfService.GetShelfByCodeAsync(code);
            return Ok(shelf);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShelfReadDto>>> GetAllAsync()
        {
            var shelves = await _shelfService.GetAllShelvesAsync();
            return Ok(shelves);
        }
    }
}
