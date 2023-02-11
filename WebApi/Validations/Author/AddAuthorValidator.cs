using FluentValidation;
using System;
using WebApi.Dto;
using WebApi.Dtos;

namespace WebApi.Validations.Author
{
    public class AddAuthorValidator : AbstractValidator<AuthorDto>
    {
        public AddAuthorValidator()
        {
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .MinimumLength(2).WithMessage("Surname must be at least 2 characters long.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");
        }
    }
}
