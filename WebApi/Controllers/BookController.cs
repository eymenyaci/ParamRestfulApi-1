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
            if (!_books.Any())
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Data Not Found!" });
                return BadRequest();
            }

            _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = _books.Count() + " Data Listed" });
            return Ok(_books);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
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
            _books.Remove(deleteById);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
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
            if (id != book.Id)
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Invalid object id!" });
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Invalid object request!" });
                return BadRequest(ModelState);
            }
            var updatedBook = _books.FirstOrDefault(x => x.Id == id);
            if (updatedBook == null)
            {
                _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Given object not found!" });
                return NotFound();
            }
            _logs.Add(new Log() { dateTime = DateTime.UtcNow, text = "Given object succesfuly updated!" });
            updatedBook = book;
            return NoContent();

        }
    }
}
