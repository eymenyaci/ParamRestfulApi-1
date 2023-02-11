using MediatR;
using WebApi.Dtos;

namespace WebApi.Commands.Model.Author
{
    public class GetAuthorCommand : IRequest<AuthorDto>
    {
        public int Id { get; set; }
    }
}
