using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Genre;
using WebApi.Dtos;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Commands.Handler.Genre
{
    public class GetGenreListCommandHandler : IRequestHandler<GetGenreListCommand, List<GenreDto>>
    {
        private readonly IGenreService _genreService;

        public GetGenreListCommandHandler(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<List<GenreDto>> Handle(GetGenreListCommand request, CancellationToken cancellationToken)
        {
            var genreList = await _genreService.GetAllGenres();
            return genreList.ToModel();
        }
    }
}
