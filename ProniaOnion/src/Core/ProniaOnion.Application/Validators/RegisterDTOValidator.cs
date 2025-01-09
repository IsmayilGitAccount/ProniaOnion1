using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProniaOnion.Application.DTOs.AppUsers;

namespace ProniaOnion.Application.Validators
{
    public class RegisterDTOValidator:AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .Matches(@"^[A-Za-z]*$");

            RuleFor(r => r.Surname)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .Matches(@"^[A-Za-z]*$");

            RuleFor(r => r.UserName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(256)
                .Matches(@"^[A-Za-z0-9-._@+]*$");

            RuleFor(r => r.Email)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(256)
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);
        }
    }
}
