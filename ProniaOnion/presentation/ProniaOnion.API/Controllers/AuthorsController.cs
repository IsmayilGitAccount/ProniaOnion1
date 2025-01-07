using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Application.DTOs.Categories;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

        [HttpGet]

        public async Task<IActionResult> Get(int page = 1, int take = 2)
        {
            var authorDTOs = await _service.GetAllAsync(page, take);

            return StatusCode(StatusCodes.Status200OK, authorDTOs);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            if (id < 1) return BadRequest();

            var authorDTO = await _service.GetByIdAsync(id);

            if (authorDTO == null) return NotFound();

            return StatusCode(StatusCodes.Status200OK, authorDTO);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromForm] CreateAuthorDTO authorDTO)
        {
            await _service.CreateAsync(authorDTO);

            return StatusCode(StatusCodes.Status201Created);


        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();

            await _service.DeleteAsync(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromForm] UpdateAuthorDTO authorDTO)
        {

            if (id < 1) return BadRequest();


            await _service.UpdateAsync(id, authorDTO);


            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
