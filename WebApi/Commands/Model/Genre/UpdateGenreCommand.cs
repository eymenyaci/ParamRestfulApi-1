using MediatR;
using WebApi.Dtos;

namespace WebApi.Commands.Model.Genre
{
    public class UpdateGenreCommand : IRequest<GenreDto>
    {
        public GenreDto Model { get; set; }
    }
}
