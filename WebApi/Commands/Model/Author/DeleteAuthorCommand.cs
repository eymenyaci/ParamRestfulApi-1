using MediatR;
using WebApi.Dtos;

namespace WebApi.Commands.Model.Author
{
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public AuthorDto Model { get; set; }
    }
}
