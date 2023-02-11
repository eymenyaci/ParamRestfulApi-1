using FluentValidation;
using WebApi.Dtos;

namespace WebApi.Validations.Genre
{
    public class GetGenreValidator : AbstractValidator<GenreDto>
    {
        public GetGenreValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
