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
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, AuthorDto>
    {
        private readonly IAuthorService _authorService;

        public UpdateAuthorCommandHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<AuthorDto> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorService.GetAuthorById(request.Model.Id);
            author.Name = request.Model.Name;
            author.Surname = request.Model.Surname;
            author.DateOfBirth = request.Model.DateOfBirth;
            author = request.Model.ToEntity(author);
            await _authorService.UpdateAuthor(author);

            return author.ToModel();
        }
    }
}
