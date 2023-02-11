using MediatR;
using System.Collections.Generic;
using WebApi.Dtos;

namespace WebApi.Commands.Model.Author
{
    public class GetAuthorListCommand : IRequest<List<AuthorDto>>
    {
    }
}
