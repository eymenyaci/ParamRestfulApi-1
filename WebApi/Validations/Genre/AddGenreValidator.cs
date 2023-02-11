using FluentValidation;
using WebApi.Dtos;

namespace WebApi.Validations.Genre
{
    public class AddGenreValidator : AbstractValidator<GenreDto>
    {
        public AddGenreValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long.");
        }
    }
}
