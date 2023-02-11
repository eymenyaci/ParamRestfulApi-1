using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using WebApi.Validations;
using WebApi.Validations.Book;

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
            var bookList = await _mediator.Send(new GetBookListCommand());
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
            var book = await _mediator.Send(new GetBookCommand { Id = id });
            var validator = new AddBookValidator();
            var result = validator.Validate(book);
            if (!result.IsValid)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }
            if (book == null)
            {
                return NotFound(id);
            }
            return Ok(book);
        }

        [SwaggerOperation(summary: "Delete entity from Book", OperationId = "DeleteBook")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var book = await _mediator.Send(new GetBookCommand { Id = id });
            var validator = new DeleteBookValidator();
            var result = validator.Validate(book);
            if (book == null)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
                return NotFound(errorMessage);
            }
            await _mediator.Send(new DeleteBookCommand() { Model = book });
            return Ok();
        }

        [SwaggerOperation(summary: "Add new entity to Book", OperationId = "InsertBook")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookDto model)
        {   
            var validator = new AddBookValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }
            model = await _mediator.Send(new AddBookCommand() { Model = model });
            return Ok(model);

        }

        [SwaggerOperation(summary: "Update entity in Book", OperationId = "UpdateBook")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BookDto model)
        {
            
            var updatedBook = await _mediator.Send(new GetBookCommand() { Id = model.Id });
            var validator = new UpdateBookValidator();
            var result = validator.Validate(model);

            if (updatedBook == null)
            {
                return NotFound();
            }
            if (!result.IsValid)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }
            model = await _mediator.Send(new UpdateBookCommand() { Model = model });
            return Ok(model);


        }


    }
}
