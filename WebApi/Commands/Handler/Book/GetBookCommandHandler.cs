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
            var book = await _bookService.GetBookById(request.Id);
            return book.ToModel();
        }
    }
}
