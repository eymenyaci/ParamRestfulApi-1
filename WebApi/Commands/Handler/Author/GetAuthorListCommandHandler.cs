using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Author;
using WebApi.Dtos;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Commands.Handler.Author
{
    public class GetAuthorListCommandHandler : IRequestHandler<GetAuthorListCommand, List<AuthorDto>>
    {
        private readonly IAuthorService _authorService;

        public GetAuthorListCommandHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<List<AuthorDto>> Handle(GetAuthorListCommand request, CancellationToken cancellationToken)
        {
            var authorList = await _authorService.GetAllAuthors();
            return authorList.ToModel();
        }
    }
}
