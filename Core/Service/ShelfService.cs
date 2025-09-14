using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions.ShelfExceptions;
using DomainLayer.Models.ShelfModule;
using ServiceAbstraction;
using Shared.DataTransfareObjects.ShelfModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ShelfService(IUnitOfWork _unitOfWork, IMapper _mapper) : IShelfService
    {
        public async Task<ShelfReadDto> CreateShelfAsync(ShelfCreateDto shelfCreateDto)
        {
            var shelf = _mapper.Map<Shelf>(shelfCreateDto);
            await _unitOfWork.ShelfRepository.AddAsync(shelf);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ShelfReadDto>(shelf);
        }

        public async Task<ShelfReadDto> UpdateShelfAsync(string code, ShelfUpdateDto shelfUpdateDto)
        {
            var shelf = await _unitOfWork.ShelfRepository.GetByCodeAsync(code);
            if (shelf is null)
                throw new ShelfNotFoundException(code);

            _mapper.Map(shelfUpdateDto, shelf);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ShelfReadDto>(shelf);
        }

        public async Task<bool> DeleteShelfAsync(string code)
        {
            var shelf = await _unitOfWork.ShelfRepository.GetByCodeAsync(code);
            if (shelf is null)
                return false;

            await _unitOfWork.ShelfRepository.DeleteAsync(code);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<ShelfReadDto> GetShelfByCodeAsync(string code)
        {
            var shelf = await _unitOfWork.ShelfRepository.GetByCodeAsync(code);
            if (shelf is null)
                throw new ShelfNotFoundException(code);

            return _mapper.Map<ShelfReadDto>(shelf);
        }

        public async Task<IEnumerable<ShelfReadDto>> GetAllShelvesAsync()
        {
            var shelves = await _unitOfWork.ShelfRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ShelfReadDto>>(shelves);
        }
    }
}
