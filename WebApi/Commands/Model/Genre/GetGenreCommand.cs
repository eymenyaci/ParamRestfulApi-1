using MediatR;
using WebApi.Dtos;

namespace WebApi.Commands.Model.Genre
{
    public class GetGenreCommand : IRequest<GenreDto>
    {
        public int Id { get; set; }

    }
}
