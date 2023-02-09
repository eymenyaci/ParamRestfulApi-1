using MediatR;
using WebApi.Dto;

namespace WebApi.Commands.Model.Book
{
    public class AddBookingCommand : IRequest<BookDto>
    {
        public BookDto Model { get; set; }
    }
}
