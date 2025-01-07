using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Application.DTOs.Blogs;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _service;

        public BlogsController(IBlogService service)
        {
            _service = service;
        }

        [HttpGet]

        public async Task<IActionResult> Get(int page = 1, int take = 2)
        {
            var blogDTOs = await _service.GetAllAsync(page, take);

            return StatusCode(StatusCodes.Status200OK, blogDTOs);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            if (id < 1) return BadRequest();

            var blogDTO = await _service.GetByIdAsync(id);

            if (blogDTO == null) return NotFound();

            return StatusCode(StatusCodes.Status200OK, blogDTO);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromForm] CreateBlogDTO blogDTO)
        {
            await _service.CreateAsync(blogDTO);

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

        public async Task<IActionResult> Update(int id, [FromForm] UpdateBlogDTO blogDTO)
        {

            if (id < 1) return BadRequest();


            await _service.UpdateAsync(id, blogDTO);


            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
