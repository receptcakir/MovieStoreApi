using FluentValidation;
using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Service.Validations
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("A valid email address is required.")
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Password)
                .Equal(x=>x.ConfirmPassword).WithMessage("Passwords do not match")
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
