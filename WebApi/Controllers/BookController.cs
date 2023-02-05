using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        List<Book> _books = new List<Book>();
        List<Log> _logs = new List<Log>();

        [HttpGet]
        public IActionResult Get()
        {
            // Is any book ?
            if (!_books.Any())
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Data Not Found!" });
                return BadRequest();
            }
            //added log information
            _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = _books.Count() + " Data Listed" });
            return Ok(_books);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // book get by id
            var bookById = _books.FirstOrDefault(x => x.Id == id);
            if (bookById == null)
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "No value with id of " + id + " !" });
                return NotFound();
            }
            _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Data with an Id of " + id + " was returned!" });
            return Ok(bookById);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteById = _books.FirstOrDefault(x => x.Id == id);
            if (deleteById == null)
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "No value with id of " + id + " !" });
                return NotFound();
            }
            _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Value " + deleteById.Id + " has been deleted" });
            // delete book by id
            _books.Remove(deleteById);
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
            _books.Add(book);
            return CreatedAtAction("Get", new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book book)
        {
            // is any book id
            if (id != book.Id)
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Invalid object id!" });
                return BadRequest();
            }
            // is any valid state
            if (!ModelState.IsValid)
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Invalid object request!" });
                return BadRequest(ModelState);
            }
            // book get by id
            var updatedBook = _books.FirstOrDefault(x => x.Id == id);
            if (updatedBook == null)
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Given object not found!" });
                return NotFound();
            }
            else
            {
                updatedBook.Id = book.Id;
                updatedBook.BookName = book.BookName;
                updatedBook.Author = book.Author;
            }
            _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Given object succesfuly updated!" });
            updatedBook = book;
            return NoContent();

        }
    }
}
