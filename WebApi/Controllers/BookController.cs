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
using WebApi.Validations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly BookDtoValidator _validator;
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
            _validator = new BookDtoValidator();
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
            var validationResult = _validator.Validate(book);
            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }
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
            var validationResult = _validator.Validate(book);
            if (book == null)
            {
                var errorMessage = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                return NotFound(errorMessage);
            }
            await _mediator.Send(new DeleteBookCommand() { Model = book });
            return Ok();
        }

        [SwaggerOperation(summary: "Add new entity to Book", OperationId = "InsertBook")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookDto model)
        {
            var validationResult = _validator.Validate(model);
            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }
            model = await _mediator.Send(new AddBookingCommand() { Model = model });
            return Ok(model);

        }

        [SwaggerOperation(summary: "Update entity in Book", OperationId = "UpdateBook")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BookDto model)
        {
            
            var updatedBook = await _mediator.Send(new GetBookQuery() { Id = model.Id });
            var validationResult = _validator.Validate(model);

            if (updatedBook == null)
            {
                return NotFound();
            }
            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }
            model = await _mediator.Send(new UpdateBookCommand() { Model = model });
            return Ok(model);


        }


    }
}
