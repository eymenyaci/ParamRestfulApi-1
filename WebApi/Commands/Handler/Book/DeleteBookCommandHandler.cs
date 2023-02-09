using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Book;
using WebApi.Interfaces;

namespace WebApi.Commands.Handler.Book
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBookService _bookService;

        public DeleteBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetBookById(request.Model.Id);
            if (book != null)
            {
                await _bookService.DeleteBook(book.Id);
            }

            return true;
        }
    }
}
