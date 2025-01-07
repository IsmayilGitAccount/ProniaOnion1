using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProniaOnion.Application.DTOs.Authors;

namespace ProniaOnion.Application.Validators
{
    public class CreateAuthorDTOValidator:AbstractValidator<CreateAuthorDTO>
    {
        public CreateAuthorDTOValidator()
        {
            RuleFor(c => c.Name)
               .MaximumLength(100)
               .WithMessage("Name must be less than 100!")
               .MinimumLength(3)
               .NotEmpty()
               .WithMessage("Data required!")
               .Matches(@"^[A-Za-z\s0-9]*$")
               .WithMessage("Name cannot contain special characters like @!");
        }
    }
}
