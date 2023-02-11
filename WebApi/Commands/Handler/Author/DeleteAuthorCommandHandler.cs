using MediatR;
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

        public DeleteAuthorCommandHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorService.GetAuthorById(request.Model.Id);
            if (author is not null)
            {
                await _authorService.DeleteAuthor(author.Id);
            }

            return true;
        }
    }
}
