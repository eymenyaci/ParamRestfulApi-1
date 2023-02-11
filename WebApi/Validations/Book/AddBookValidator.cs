using FluentValidation;
using System;
using WebApi.Dto;

namespace WebApi.Validations.Book
{
    public class AddBookValidator : AbstractValidator<BookDto>
    {
        public AddBookValidator()
        {
            RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.GenreId)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long.");

            RuleFor(x => x.PageCount)
                .GreaterThan(0).WithMessage("Page count must be greater than 0");

            RuleFor(x => x.PublishDate)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");
        }
    }
}
