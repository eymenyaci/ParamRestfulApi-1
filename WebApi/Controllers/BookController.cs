using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("test/custom")]
        public string ExceptionTest()
        {
            int a = 0;
            int b = 5 / a;

            return "OK";
        }
        [HttpGet]
        public IActionResult Get()
        {
            var books = _bookService.GetAllBooks();
            // Is any book ?
            if (!books.Any())
            {
                return BadRequest();
            }
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // book get by id
            var bookById = _bookService.GetBookById(id);
            if (bookById == null)
            {
                return NotFound();
            }
            return Ok(bookById);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromQuery] int id)
        {
            var deleteById = _bookService.GetBookById(id);
            if (deleteById == null)
            {
                return NotFound();
            }
            // delete book by id
            _bookService.DeleteBook(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            // model is valid ?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _bookService.CreateBook(book);
            return CreatedAtAction("Get", new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book book)
        {
            // is any valid state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // book get by id
            var updatedBook = _bookService.GetBookById(book.Id);
            if (updatedBook == null)
            {
                return NotFound();
            }
            
            _bookService.UpdateBook(book);
            return NoContent();

        }
    }
}
