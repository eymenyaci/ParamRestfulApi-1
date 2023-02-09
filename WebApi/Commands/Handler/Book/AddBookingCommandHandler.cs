using MediatR;
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
    public class AddBookingCommandHandler : IRequestHandler<AddBookingCommand, BookDto>
    {
        private readonly IBookService _bookService;


        public AddBookingCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<BookDto> Handle(AddBookingCommand request, CancellationToken cancellationToken)
        {
            var book = request.Model.ToEntity();
            await _bookService.CreateBook(book);
            return book.ToModel();

        }
    }
}
