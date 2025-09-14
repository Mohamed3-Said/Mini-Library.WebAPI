using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransfareObjects.PublisherModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        // Create Endpoints for Publisher
        [HttpPost("Create")]
        public async Task<ActionResult<PublisherToReadDto>> CreateAsync(PublisherCreateDto createDto)
        {
            var publisher = await _publisherService.CreatePublisherAsync(createDto);
            return Ok(publisher);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PublisherToReadDto>> UpdateAsync(int id , PublisherUpdateDto updateDto)
        {
            var publisher = await _publisherService.UpdatePublisherAsync(id, updateDto);
            return Ok(publisher);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            var result = await _publisherService.DeletePublisherAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherToReadDto>> GetByIdAsync(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);
            return Ok(publisher);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherToReadDto>>> GetAllAsync()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            return Ok(publishers);
        }

    }
}
