using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Commands.Model.Author;
using WebApi.Commands.Model.Book;
using WebApi.Commands.Model.Genre;
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
                return NotFound();
            }
            return Ok(bookList);

        }

        [SwaggerOperation(summary: "Get entity from Book by id", OperationId = "GetBookById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // book get by id
            var book = await _mediator.Send(new GetBookCommand { Id = id, IdType = "Book" });
            
            if (book == null && id != 0)
            {
                return NotFound(id);
            }

            if (id == 0)
            {
                return BadRequest("Id cannot be 0.");
            }

            return Ok(book);


        }

        [SwaggerOperation(summary: "Delete entity from Book", OperationId = "DeleteBook")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var book = await _mediator.Send(new GetBookCommand { Id = id, IdType = "Book" });
            if (book == null && id != 0)
            {
                return NotFound(id);
            }
            if (id == 0)
            {
                return BadRequest("Id cannot be 0.");
            }
            await _mediator.Send(new DeleteBookCommand() { Model = book });
            return Ok();
        }

        [SwaggerOperation(summary: "Add new entity to Book", OperationId = "InsertBook")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookDto model)
        {
            var isAnyAuthor = await _mediator.Send(new GetAuthorCommand() { Id = model.AuthorId });
            var isAnyGenre = await _mediator.Send(new GetGenreCommand() { Id = model.GenreId });

            if (isAnyAuthor is null || isAnyGenre is null)
            {
                return BadRequest("The author or genre cannot be created as they have books associated with them");
            }

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
            var validator = new UpdateBookValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }

            var updatedBook = await _mediator.Send(new GetBookCommand() { Id = model.Id, IdType = "Book" });
            if (updatedBook == null)
            {
                return NotFound();
            }

            var isAnyAuthor = await _mediator.Send(new GetAuthorCommand() { Id = model.AuthorId });
            var isAnyGenre = await _mediator.Send(new GetGenreCommand() { Id = model.GenreId });
            if (isAnyAuthor is null || isAnyGenre is null)
            {
                return BadRequest("The author or genre cannot be deleted as they have books associated with them");
            }

            model = await _mediator.Send(new UpdateBookCommand() { Model = model });
            return Ok(model);


        }


    }
}
