using MediatR;
using WebApi.Dto;

namespace WebApi.Commands.Model.Book
{
    public class GetBookCommand : IRequest<BookDto>
    {
        public int Id { get; set; }

        public string IdType { get; set; }
    }
}
