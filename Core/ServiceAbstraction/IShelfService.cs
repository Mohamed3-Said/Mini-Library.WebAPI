using Shared.DataTransfareObjects.ShelfModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IShelfService
    {
         Task<ShelfReadDto> CreateShelfAsync(ShelfCreateDto shelfCreateDto);
         Task<ShelfReadDto> UpdateShelfAsync(string code, ShelfUpdateDto shelfUpdateDto);
         Task<bool> DeleteShelfAsync(string code);
         Task<ShelfReadDto> GetShelfByCodeAsync(string code);
         Task<IEnumerable<ShelfReadDto>> GetAllShelvesAsync();
    }
}
