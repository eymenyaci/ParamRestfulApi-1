using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Genre;
using WebApi.Interfaces;
using WebApi.Models.Entity;
using WebApi.Services;

namespace WebApi.Commands.Handler.Genre
{
    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, bool>
    {
        private readonly IGenreService _genreService;
        private readonly IBookService _bookService;

        public DeleteGenreCommandHandler(IGenreService genreService, IBookService bookService)
        {
            _genreService = genreService;
            _bookService = bookService;
        }

        public async Task<bool> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreService.GetGenreById(request.Model.Id);
            bool isAnyGenre = _bookService.IsAnyGenre(request.Model.Id);
            if (genre is not null && isAnyGenre is false)
            {
                await _genreService.DeleteGenre(genre.Id);
            }
            if (genre is null)
                throw new ArgumentException("Genre not found");

            if (isAnyGenre is true)
                throw new InvalidOperationException("The genre cannot be deleted as they have books associated with them");

            return true;
        }
    }
}
