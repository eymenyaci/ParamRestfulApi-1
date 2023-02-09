using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Book;
using WebApi.Dto;
using WebApi.Extensions;
using WebApi.Interfaces;

namespace WebApi.Commands.Handler.Book
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDto>
    {
        private readonly IBookService _bookService;

        public GetBookQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<BookDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetBookById(request.Id);
            return book.ToModel();
        }
    }
}
