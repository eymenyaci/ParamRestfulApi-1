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
        List<Log> _logs = new List<Log>();

        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var books = _bookService.GetAllBooks();
            // Is any book ?
            if (!books.Any())
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Data Not Found!" });
                return BadRequest();
            }
            //added log information
            _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = books.Count() + " Data Listed" });
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // book get by id
            var bookById = _bookService.GetBookById(id);
            if (bookById == null)
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "No value with id of " + id + " !" });
                return NotFound();
            }
            _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Data with an Id of " + id + " was returned!" });
            return Ok(bookById);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromQuery] int id)
        {
            var deleteById = _bookService.GetBookById(id);
            if (deleteById == null)
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "No value with id of " + id + " !" });
                return NotFound();
            }
            _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Value " + deleteById.Id + " has been deleted" });
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
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Invalid object request!" });
                return BadRequest(ModelState);
            }
            _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Data successfully added!" });
            _bookService.CreateBook(book);
            return CreatedAtAction("Get", new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book book)
        {
            // is any valid state
            if (!ModelState.IsValid)
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Invalid object request!" });
                return BadRequest(ModelState);
            }
            // book get by id
            var updatedBook = _bookService.GetBookById(book.Id);
            if (updatedBook == null)
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Given object not found!" });
                return NotFound();
            }
            
            _bookService.UpdateBook(book);
            _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Given object succesfuly updated!" });
            return NoContent();

        }
    }
}
