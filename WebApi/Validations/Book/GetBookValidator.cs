using FluentValidation;
using WebApi.Dto;

namespace WebApi.Validations.Book
{
    public class GetBookValidator : AbstractValidator<BookDto>
    {
        public GetBookValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.")
            .NotEqual(0).WithMessage("Id cannot be 0.");
        }
    }
}
