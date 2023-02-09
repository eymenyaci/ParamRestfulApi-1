using MediatR;
using WebApi.Dto;

namespace WebApi.Commands.Model.Book
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public BookDto Model { get; set; }
    }
}
