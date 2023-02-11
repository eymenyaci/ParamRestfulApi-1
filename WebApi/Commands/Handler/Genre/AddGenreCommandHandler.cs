using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Author;
using WebApi.Commands.Model.Genre;
using WebApi.Dtos;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Commands.Handler.Genre
{
    public class AddGenreCommandHandler : IRequestHandler<AddGenreCommand, GenreDto>
    {
        private readonly IGenreService _genreService;

        public AddGenreCommandHandler(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<GenreDto> Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = request.Model.ToEntity();
            await _genreService.CreateGenre(genre);
            return genre.ToModel();
        }
    }
}
