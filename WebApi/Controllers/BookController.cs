using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Commands.Model.Book;
using WebApi.Dto;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Models.Entity;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BookController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerOperation(summary: "Get entities from Book", OperationId = "GetBooks")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bookList = await _mediator.Send(new GetBookListQuery());
            //Is any book ?
            if (!bookList.Any())
            {
                return BadRequest();
            }
            return Ok(bookList);

        }

        [SwaggerOperation(summary: "Get entity from Book by id", OperationId = "GetBookById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // book get by id
            var book = await _mediator.Send(new GetBookQuery { Id = id });
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);    
        }

        [SwaggerOperation(summary: "Delete entity from Book", OperationId = "DeleteBook")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var book = await _mediator.Send(new GetBookQuery { Id = id });
            if (book == null)
            {
                return NotFound();
            }
            await _mediator.Send(new DeleteBookCommand() { Model = book });
            return Ok();
        }

        [SwaggerOperation(summary: "Add new entity to Book", OperationId = "InsertBook")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookDto model)
        {
            // model is valid ?
            if (ModelState.IsValid)
            {
                model = await _mediator.Send(new AddBookingCommand() { Model = model });
                return Ok(model);
            }
            return BadRequest(ModelState);
        }

        [SwaggerOperation(summary: "Update entity in Book", OperationId = "UpdateBook")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BookDto model)
        {
            // is any valid state
            var updatedBook = await _mediator.Send(new GetBookQuery() { Id = model.Id });
            if (updatedBook == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                model = await _mediator.Send(new UpdateBookCommand() { Model = model }); 
                return Ok (model);
            }

            return BadRequest(ModelState);
            

        }

        
    }
}
