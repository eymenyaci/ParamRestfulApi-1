using MediatR;
using WebApi.Dtos;

namespace WebApi.Commands.Model.Genre
{
    public class AddGenreCommand : IRequest<GenreDto>
    {
        public GenreDto Model { get; set; }
    }
}
