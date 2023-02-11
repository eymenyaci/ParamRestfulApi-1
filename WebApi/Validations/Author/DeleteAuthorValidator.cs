using FluentValidation;
using WebApi.Dtos;

namespace WebApi.Validations.Author
{
    public class DeleteAuthorValidator : AbstractValidator<AuthorDto>
    {
        public DeleteAuthorValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
