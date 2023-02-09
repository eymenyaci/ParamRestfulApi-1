using MediatR;
using WebApi.Dto;

namespace WebApi.Commands.Model.Book
{
    public class GetBookQuery : IRequest<BookDto>
    {
        public int Id { get; set; }
    }
}
