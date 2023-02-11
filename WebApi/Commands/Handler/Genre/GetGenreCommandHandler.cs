using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Genre;
using WebApi.Dtos;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Commands.Handler.Genre
{
    public class GetGenreCommandHandler : IRequestHandler<GetGenreCommand, GenreDto>
    {
        private readonly IGenreService _genreService;

        public GetGenreCommandHandler(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<GenreDto> Handle(GetGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreService.GetGenreById(request.Id);
            return genre.ToModel();
        }
    }
}
