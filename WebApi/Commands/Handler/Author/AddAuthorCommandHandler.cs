using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Author;
using WebApi.Dtos;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Commands.Handler.Author
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, AuthorDto>
    {
        private readonly IAuthorService _authorService;

        public AddAuthorCommandHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<AuthorDto> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = request.Model.ToEntity();
            await _authorService.CreateAuthor(author);
            return author.ToModel();
        }
    }
}
