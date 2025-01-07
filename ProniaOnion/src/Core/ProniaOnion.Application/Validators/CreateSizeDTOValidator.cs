using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProniaOnion.Application.DTOs.Sizes;

namespace ProniaOnion.Application.Validators
{
    public class CreateSizeDTOValidator:AbstractValidator<CreateSizeDTO>
    {
        public CreateSizeDTOValidator()
        {
            RuleFor(c => c.Name)
                .MaximumLength(100)
                .WithMessage("Name must be less than 100!")
                .NotEmpty()
                .WithMessage("Data required!")
                .Matches(@"^[A-Za-z\s0-9]*$")
                .WithMessage("Name cannot contain special characters like @!");
        }
    }
}
