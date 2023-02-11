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
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, GenreDto>
    {
        private readonly IGenreService _genreService;

        public UpdateGenreCommandHandler(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<GenreDto> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreService.GetGenreById(request.Model.Id);
            genre.Name = request.Model.Name;
            genre.IsActive = true;
            genre = request.Model.ToEntity(genre);
            await _genreService.UpdateGenre(genre);

            return genre.ToModel();
        }
    }
}
