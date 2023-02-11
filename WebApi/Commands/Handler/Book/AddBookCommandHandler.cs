using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Book;
using WebApi.Dto;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Models.Context;
using WebApi.Models.Entity;

namespace WebApi.Commands.Handler.Book
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, BookDto>
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;

        public AddBookCommandHandler(IBookService bookService, IAuthorService authorService, IGenreService genreService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
        }

        public async Task<BookDto> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = request.Model.ToEntity();
            bool isAnyAuthor = _authorService.IsAnyAuthor(request.Model.AuthorId);
            bool isAnyGenre = _genreService.IsAnyGenre(request.Model.GenreId);
            
            if (book is not null && isAnyAuthor is true && isAnyGenre is true)
            {
                await _bookService.CreateBook(book);
            }

            if (book is null)
                throw new NullReferenceException(nameof(book));
            if (isAnyAuthor is false)
                throw new ArgumentException("Author not found");
            if (isAnyGenre is false)
                throw new ArgumentException("Genre not found");

            return book.ToModel();

        }
    }
}
