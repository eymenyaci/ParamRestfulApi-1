using FluentValidation;
using WebApi.Dto;
using WebApi.Interfaces;

namespace WebApi.Validations
{
    public class BookDtoValidator : AbstractValidator<BookDto>
    {
        public BookDtoValidator()
        {
            RuleFor(x => x.BookName)
            .NotEmpty().WithMessage("Book name cannot be empty")
            .Length(2, 50).WithMessage("Book name must be between 2 and 50 characters");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author name cannot be empty")
                .Length(2, 50).WithMessage("Author name must be between 2 and 50 characters");

            RuleFor(x => x.PageCount)
                .GreaterThan(0).WithMessage("Page count must be greater than 0");

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
