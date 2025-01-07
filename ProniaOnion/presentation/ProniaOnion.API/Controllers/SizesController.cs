using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Application.DTOs.Sizes;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ISizeService _service;

        public SizesController(ISizeService service)
        {
            _service = service;
        }

        [HttpGet]

        public async Task<IActionResult> Get(int page = 1, int take = 20)
        {
            var sizes = await _service.GetAllAsync(page, take);
            return StatusCode(StatusCodes.Status200OK, sizes);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            if (id < 1) return StatusCode(StatusCodes.Status400BadRequest);

            var sizes = await _service.GetByIdAsync(id);

            if (sizes is null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, sizes);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromForm] CreateSizeDTO sizeDTO)
        {
            await _service.CreateAsync(sizeDTO);

            return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }

        [HttpPut]

        public async Task<IActionResult> Update(int id, UpdateSizeDTO sizeDTO)
        {
            if (id < 1) return BadRequest();

            await _service.UpdateAsync(id, sizeDTO);

            return NoContent();
        }
    }
}
