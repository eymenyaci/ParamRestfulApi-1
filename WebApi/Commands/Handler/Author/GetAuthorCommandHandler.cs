using MediatR;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Author;
using WebApi.Dtos;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Commands.Handler.Author
{
    public class GetAuthorCommandHandler : IRequestHandler<GetAuthorCommand, AuthorDto>
    {
        private readonly IAuthorService _authorService;

        public GetAuthorCommandHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<AuthorDto> Handle(GetAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorService.GetAuthorById(request.Id);
            return author.ToModel();
        }
    }
}
