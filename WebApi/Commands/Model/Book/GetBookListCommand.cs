using MediatR;
using System.Collections.Generic;
using WebApi.Dto;

namespace WebApi.Commands.Model.Book
{
    public class GetBookListCommand : IRequest<List<BookDto>>
    {

    }
}
