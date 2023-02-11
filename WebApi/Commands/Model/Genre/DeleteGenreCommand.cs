using MediatR;
using WebApi.Dtos;

namespace WebApi.Commands.Model.Genre
{
    public class DeleteGenreCommand : IRequest<bool>
    {
        public GenreDto Model { get; set; }
    }
}
