using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProniaOnion.Application.DTOs.Products;

namespace ProniaOnion.Application.Validators
{
    public class CreateProductDTOValidator:AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidator()
        {
            RuleFor(p=>p.Name)
                .NotEmpty()
                    .WithMessage("Name is required")
                .MaximumLength(100)
                    .WithMessage("Name should be less than 100 characters!");

            RuleFor(p => p.SKU)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(10);

            RuleFor(p => p.Description)
                .NotEmpty();

            RuleFor(p=>p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(9999.99m);

            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .Must(categoryId => categoryId > 0);

            RuleForEach(p=>p.ColorsIds)
                .NotEmpty()
                .Must(colorId => colorId > 0);

            RuleFor(p => p.ColorsIds)
                .NotEmpty()
                .Must(ci => ci.Count > 0);
        }
    }
}
