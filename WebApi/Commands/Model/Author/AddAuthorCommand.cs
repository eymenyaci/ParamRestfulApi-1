using MediatR;
using WebApi.Dtos;

namespace WebApi.Commands.Model.Author
{
    public class AddAuthorCommand : IRequest<AuthorDto>
    {
        public AuthorDto Model { get; set; }
    }
}
