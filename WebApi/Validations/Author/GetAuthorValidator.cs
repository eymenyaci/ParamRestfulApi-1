using FluentValidation;
using WebApi.Dtos;

namespace WebApi.Validations.Author
{
    public class GetAuthorValidator : AbstractValidator<AuthorDto>
    {
        public GetAuthorValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
