using Shared.DataTransfareObjects.PublisherModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IPublisherService
    {
        Task<PublisherToReadDto> CreatePublisherAsync(PublisherCreateDto publisherCreateDto);
        Task<PublisherToReadDto> UpdatePublisherAsync(int id, PublisherUpdateDto publisherUpdateDto);
        Task<bool> DeletePublisherAsync(int id);
        Task<PublisherToReadDto?> GetPublisherByIdAsync(int id);
        Task<IEnumerable<PublisherToReadDto>> GetAllPublishersAsync();
    }
}
