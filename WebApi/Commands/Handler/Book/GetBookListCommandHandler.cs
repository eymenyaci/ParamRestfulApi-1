using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Commands.Model.Book;
using WebApi.Dto;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Models.Entity;

namespace WebApi.Commands.Handler.Book
{
    public class GetBookListCommandHandler : IRequestHandler<GetBookListCommand, List<BookDto>>
    {
        private readonly IBookService _bookService;

        public GetBookListCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<List<BookDto>> Handle(GetBookListCommand request, CancellationToken cancellationToken)
        {
            var bookList = await _bookService.GetAllBooks();
            return bookList.ToModel();
        }
    }
}
