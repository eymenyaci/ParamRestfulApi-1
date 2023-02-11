using MediatR;
using WebApi.Dto;

namespace WebApi.Commands.Model.Book
{
    public class AddBookCommand : IRequest<BookDto>
    {
        public BookDto Model { get; set; }
    }
}
