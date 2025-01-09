using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProniaOnion.Application.DTOs.AppUsers;

namespace ProniaOnion.Application.Validators
{
    public class LoginDTOValidation:AbstractValidator<LoginDTO>
    {
        public LoginDTOValidation()
        {
            RuleFor(l=>l.UserNameofEmail)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(256);

            RuleFor(l => l.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);
        }
    }
}
