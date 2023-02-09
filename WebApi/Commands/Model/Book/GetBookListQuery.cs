using MediatR;
using System.Collections.Generic;
using WebApi.Dto;

namespace WebApi.Commands.Model.Book
{
    public class GetBookListQuery : IRequest<List<BookDto>>
    {

    }
}
