using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.Validators
{
    public class CreateCategoryDTOValidator:AbstractValidator<CreateCategoryDTO>
    {
        private readonly ICategoryRepository _repository;

        public CreateCategoryDTOValidator(ICategoryRepository repository)
        {
            _repository = repository;

            RuleFor(c => c.Name)
                .MaximumLength(100)
                .WithMessage("Name must be less than 100!")
                .MinimumLength(3)
                .NotEmpty()
                .WithMessage("Data required!")
                .Matches(@"^[A-Za-z\s0-9]*$")
                .WithMessage("Name cannot contain special characters like @!");
                //.MustAsync(CheckName)
                //.WithMessage("Category already exist");
        }

        //public async Task<bool> CheckName(string name, CancellationToken cancellationToken)
        //{
        //    return !await _repository.AnyAsync(c=>c.Name == name);   
        //} 
    }
}
