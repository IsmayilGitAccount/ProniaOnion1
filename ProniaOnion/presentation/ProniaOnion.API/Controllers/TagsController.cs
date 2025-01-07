using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Application.DTOs.Tags;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]

        public async Task<IActionResult> Get(int page = 1, int take = 20)
        {
            var tags = await _service.GetAllAsync(page, take);
            return StatusCode(StatusCodes.Status200OK, tags);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            if (id < 1) return StatusCode(StatusCodes.Status400BadRequest);

            var tags = await _service.GetByIdAsync(id);

            if (tags is null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, tags);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromForm] CreateTagDTO tagDTO)
        {
            await _service.CreateAsync(tagDTO);

            return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }

        [HttpPut]

        public async Task<IActionResult> Update(int id, UpdateTagDTO tagDTO)
        {
            if (id < 1) return BadRequest();

            await _service.UpdateAsync(id, tagDTO);

            return NoContent();
        }
    }
}
