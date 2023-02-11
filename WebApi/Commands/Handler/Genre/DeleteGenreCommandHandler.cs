using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Genre;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Commands.Handler.Genre
{
    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, bool>
    {
        private readonly IGenreService _genreService;

        public DeleteGenreCommandHandler(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<bool> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreService.GetGenreById(request.Model.Id);
            if (genre is not null)
            {
                await _genreService.DeleteGenre(genre.Id);
            }

            return true;
        }
    }
}
