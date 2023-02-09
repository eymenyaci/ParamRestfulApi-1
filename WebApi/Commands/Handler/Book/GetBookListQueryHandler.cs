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
    public class GetBookListQueryHandler : IRequestHandler<GetBookListQuery, List<BookDto>>
    {
        private readonly IBookService _bookService;

        public GetBookListQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<List<BookDto>> Handle(GetBookListQuery request, CancellationToken cancellationToken)
        {
            var bookList = await _bookService.GetAllBooks();
            return bookList.ToModel();
        }

        //private List<BookDto> ConvertToBookDto(List<Book> bookList)
        //{
        //    return bookList.Select(book => new BookDto {
        //    Id = book.Id,
        //    BookName = book.BookName,
        //    Author = book.Author,
        //    PageCount = book.PageCount
        //    }).ToList();
        //}
    }
}
