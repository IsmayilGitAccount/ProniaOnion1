using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Blogs;
using ProniaOnion.Application.DTOs.Genres;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenresController(IGenreService service)
        {
            _service = service;
        }

        [HttpGet]

        public async Task<IActionResult> Get(int page = 1, int take = 2)
        {
            var genreDTOs = await _service.GetAllAsync(page, take);

            return StatusCode(StatusCodes.Status200OK, genreDTOs);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            if (id < 1) return BadRequest();

            var genreDTO = await _service.GetByIdAsync(id);

            if (genreDTO == null) return NotFound();

            return StatusCode(StatusCodes.Status200OK, genreDTO);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromForm] CreateGenreDTO genreDTO)
        {
            await _service.CreateAsync(genreDTO);

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

        public async Task<IActionResult> Update(int id, [FromForm] UpdateGenreDTO genreDTO)
        {

            if (id < 1) return BadRequest();


            await _service.UpdateAsync(id, genreDTO);


            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
