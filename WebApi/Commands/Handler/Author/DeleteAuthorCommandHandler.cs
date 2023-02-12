using MediatR;
using Microsoft.OpenApi.Any;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Author;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Commands.Handler.Author
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public DeleteAuthorCommandHandler(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorService.GetAuthorById(request.Model.Id);
            var isAnyBook = _bookService.GetBookByAuthorId(request.Model.Id);

            if (author is not null && isAnyBook is not null)
            {
                await _authorService.DeleteAuthor(author.Id);
            }
            
            if (author is null)
                throw new ArgumentException("Author not found");

            if (isAnyBook is not null)
                throw new InvalidOperationException("The author cannot be deleted as they have books associated with them");

            return true;
        }
    }
}
