using MediatR;
using WebApi.Dto;

namespace WebApi.Commands.Model.Book
{
    public class UpdateBookCommand : IRequest<BookDto>
    {
        public BookDto Model { get; set; }
    }
}
