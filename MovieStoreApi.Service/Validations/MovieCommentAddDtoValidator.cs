using FluentValidation;
using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Core.Models;

namespace MovieStoreApi.Service.Validations
{
    public class MovieCommentAddDtoValidator : AbstractValidator<MovieCommentAddDto>
    {
        public MovieCommentAddDtoValidator()
        {

            RuleFor(x => x.Comment)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 10).WithMessage("{PropertyName} must be in between 1 and 10");
   
        }
    }
}
