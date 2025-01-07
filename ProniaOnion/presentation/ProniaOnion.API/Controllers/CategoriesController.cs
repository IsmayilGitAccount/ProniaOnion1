using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Categories;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IValidator<CreateCategoryDTO> _validator;

        public CategoriesController(ICategoryService service, IValidator<CreateCategoryDTO> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]

        public async Task<IActionResult> Get(int page = 1, int take = 2)
        {
            var categoryDTOs = await _service.GetAllAsync(page, take);

            return StatusCode(StatusCodes.Status200OK, categoryDTOs);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            if (id < 1) return BadRequest();

            var categoryDTO = await _service.GetByIdAsync(id);

            if (categoryDTO == null) return NotFound();

            return StatusCode(StatusCodes.Status200OK, categoryDTO);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromForm] CreateCategoryDTO categoryDTO)
        {

            //var result = await _validator.ValidateAsync(categoryDTO);

            //if (!result.IsValid)
            //{
            //    foreach (ValidationFailure error in result.Errors)
            //    {
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            //    }

            //    return BadRequest(ModelState);
            //}

            await _service.CreateAsync(categoryDTO);

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

        public async Task<IActionResult> Update(int id, [FromForm] UpdateCategoryDTO categoryDTO)
        {

            if (id < 1) return BadRequest();


            await _service.UpdateAsync(id, categoryDTO);


            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
