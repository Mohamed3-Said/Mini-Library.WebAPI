using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions.PublisherExceptions;
using DomainLayer.Models.PublisherModule;
using ServiceAbstraction;
using Shared.DataTransfareObjects.PublisherModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PublisherService(IUnitOfWork _unitOfWork , IMapper _mapper) : IPublisherService
    {
        public async Task<PublisherToReadDto> CreatePublisherAsync(PublisherCreateDto publisherCreateDto)
        {
            var publisher = _mapper.Map<PublisherCreateDto, Publisher>(publisherCreateDto);
            await _unitOfWork.PublisherRepository.AddAsync(publisher);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Publisher, PublisherToReadDto>(publisher);
        }

        public async Task<PublisherToReadDto> UpdatePublisherAsync(int id, PublisherUpdateDto publisherUpdateDto)
        {
           var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(id);
            if (publisher is not null)
            {
                _mapper.Map<PublisherUpdateDto,Publisher>(publisherUpdateDto,publisher);
                await _unitOfWork.PublisherRepository.UpdateAsync(publisher);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<Publisher, PublisherToReadDto>(publisher);
            }
            throw new PublisherNotFoundException(id);
        }
        public async Task<bool> DeletePublisherAsync(int id)
        {
            var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(id);
            if (publisher is not null)
            {
                await _unitOfWork.PublisherRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else
                return false;

        }

        public async Task<IEnumerable<PublisherToReadDto>> GetAllPublishersAsync()
        {
            var publishers = await _unitOfWork.PublisherRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherToReadDto>>(publishers);
        }

        public async Task<PublisherToReadDto?> GetPublisherByIdAsync(int id)
        {
            var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(id);
            if(publisher is not null)
            {
              return  _mapper.Map<Publisher, PublisherToReadDto>(publisher);
            }
            throw new PublisherNotFoundException(id);
        }

    }
}
