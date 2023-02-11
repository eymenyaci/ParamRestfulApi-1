using FluentValidation;
using WebApi.Dto;

namespace WebApi.Validations.Book
{
    public class DeleteBookValidator : AbstractValidator<BookDto>
    {
        public DeleteBookValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
