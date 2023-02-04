using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        List<Book> _books = new List<Book>(){};
        List<Log> _logs =new List<Log>();
        //Dummy data created
            public BookController()
            {
                Book book1 = new Book(){
                    Id = 1,
                    BookName = "Random Book Name 1",
                    Author = "Random Author Name 1",
                    PageCount = 550   
                };
                Book book2 = new Book(){
                    Id = 2,
                    BookName = "Random Book Name 2",
                    Author = "Random Author Name 2",
                    PageCount = 450   
                };
                Book book3 = new Book(){
                    Id = 3,
                    BookName = "Random Book Name 3",
                    Author = "Random Author Name 3",
                    PageCount = 350   
                };
                Book book4 = new Book(){
                    Id = 4,
                    BookName = "Random Book Name 4",
                    Author = "Random Author Name 4",
                    PageCount = 800   
                };
                _books.Add(book1);
                _books.Add(book2);
                _books.Add(book3);
                _books.Add(book4);
                
            }

            [HttpGet]
            public IActionResult Get()
            {
                
                if (!_books.Any())
                {
                    _logs.Add(new Log(){dateTime = DateTime.UtcNow,text = "Data Not Found!"});
                    return BadRequest();
                }
                    
                _logs.Add(new Log(){dateTime = DateTime.UtcNow,text = _books.Count() + " Data Listed"});
                return Ok(_books);
            }

            [HttpGet("{id}")]
            public IActionResult Get(int id)
            {
                var bookById = _books.FirstOrDefault(x => x.Id == id);
                if (bookById == null)
                {
                    _logs.Add(new Log(){dateTime = DateTime.UtcNow,text = "No value with id of " + id + " !"});
                    return NotFound();
                }
                _logs.Add(new Log(){dateTime = DateTime.UtcNow,text = "Data with an Id of "+ id +" was returned!"});
                return Ok(bookById);
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                var deleteById = _books.FirstOrDefault(x => x.Id == id);
                if (deleteById == null)
                {
                    _logs.Add(new Log(){dateTime = DateTime.UtcNow,text = "No value with id of " + id + " !"});
                    return NotFound();
                }
                _logs.Add(new Log(){dateTime = DateTime.UtcNow,text = "Value "+deleteById.Id+" has been deleted"});
                _books.Remove(deleteById);
                return NoContent();
            }
    }
}
