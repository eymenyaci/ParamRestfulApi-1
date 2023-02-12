using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Book;
using WebApi.Dto;
using WebApi.Extensions;
using WebApi.Interfaces;

namespace WebApi.Commands.Handler.Book
{
    public class GetBookCommandHandler : IRequestHandler<GetBookCommand, BookDto>
    {
        private readonly IBookService _bookService;

        public GetBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<BookDto> Handle(GetBookCommand request, CancellationToken cancellationToken)
        {
            if (request.IdType == "Author")
            {
                //Get author id
                var authorByBook = await _bookService.GetBookByAuthorId(request.Id);
                if (authorByBook != null)
                    return authorByBook.ToModel();
            }
            if (request.IdType == "Genre")
            {
                //Get genre id
                var genreByBook = await _bookService.GetBookByGenreId(request.Id);
                if (genreByBook != null)
                    return genreByBook.ToModel();
            }
           
            var book = await _bookService.GetBookById(request.Id);
            return book.ToModel();
        }
    }
}
