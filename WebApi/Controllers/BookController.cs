using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Models.Entity;
using WebApi.Services;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        /// <summary>
        /// Get All Book values
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get book by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Book created
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update created by book and book id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Filtering according to the bookname value with the extension method
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        [HttpGet("Filter/Author/{bookName}")]
        public IActionResult GetBookByBookName(string bookName)
        {
            var books = _bookService.GetAllBooks();
            if (!books.Any())
            {
                return NotFound();
            }
            return Ok(books.FilterByBookName(bookName));
        }

        /// <summary>
        /// Filtering according to the authorName value with the extension method
        /// </summary>
        /// <param name="authorName"></param>
        /// <returns></returns>
        [HttpGet("Filter/BookName/{authorName}")]
        public IActionResult GetBookByAuthorName(string authorName)
        {
            var books = _bookService.GetAllBooks();
            if (!books.Any())
            {
                return NotFound();
            }
            return Ok(books.FilterByAuthor(authorName));
        }
    }
}
