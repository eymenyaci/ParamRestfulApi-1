using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Book;
using WebApi.Dto;
using WebApi.Extensions;
using WebApi.Interfaces;

namespace WebApi.Commands.Handler.Book
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDto>
    {
        private readonly IBookService _bookService;

        public UpdateBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetBookById(request.Model.Id);
            book.Title = book.Title;
            book.GenreId = book.GenreId;
            book.PageCount = book.PageCount;
            book.PublishDate = book.PublishDate;
            book.AuthorId = book.AuthorId;
            book = request.Model.ToEntity(book);
            await _bookService.UpdateBook(book);

            return book.ToModel();
        }
    }
}
