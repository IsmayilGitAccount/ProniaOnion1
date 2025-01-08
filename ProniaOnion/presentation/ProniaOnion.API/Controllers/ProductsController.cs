using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Products;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService productService)
        {
            _service = productService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll(int page = 1, int take = 10)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();

            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]

        public async Task<IActionResult> Post([FromForm]CreateProductDTO productDTO)
        {
            await _service.CreateAsync(productDTO);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]

        public async Task<IActionResult> Put(int id, UpdateProductDTO productDTO)
        {
            if(id < 1) return BadRequest(); 

            await _service.UpdateAsync(id, productDTO);
            return NoContent();
        }
    }
}
