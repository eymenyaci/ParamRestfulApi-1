using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Commands.Model.Author;
using WebApi.Commands.Model.Book;
using WebApi.Dto;
using WebApi.Dtos;
using WebApi.Validations.Author;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerOperation(summary: "Get entities from Author", OperationId = "GetAuthor")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var authorList = await _mediator.Send(new GetAuthorListCommand());
            //Is any author ?
            if (!authorList.Any())
            {
                return BadRequest();
            }
            return Ok(authorList);

        }

        [SwaggerOperation(summary: "Get entity from Author by id", OperationId = "GetAuthorById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // author get by id
            var author = await _mediator.Send(new GetAuthorCommand { Id = id });
            var validator = new GetAuthorValidator();
            var result = validator.Validate(author);
            if (!result.IsValid)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }
            if (author is null)
            {
                return NotFound(id);
            }
            return Ok(author);
        }

        [SwaggerOperation(summary: "Delete entity from Author", OperationId = "DeleteAuthor")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var author = await _mediator.Send(new GetAuthorCommand { Id = id });
            var validator = new DeleteAuthorValidator();
            var result = validator.Validate(author);
            if (author is null)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
                return NotFound(errorMessage);
            }
            await _mediator.Send(new DeleteAuthorCommand() { Model = author });
            return Ok();
        }

        [SwaggerOperation(summary: "Add new entity to Author", OperationId = "InsertAuthor")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorDto model)
        {
            var validator = new AddAuthorValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }
            model = await _mediator.Send(new AddAuthorCommand() { Model = model });
            return Ok(model);

        }

        [SwaggerOperation(summary: "Update entity in Author", OperationId = "UpdateAuthor")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AuthorDto model)
        {

            var updatedBook = await _mediator.Send(new GetBookCommand() { Id = model.Id });
            var validator = new UpdateAuthorValidator();
            var result = validator.Validate(model);

            if (updatedBook is null)
            {
                return NotFound();
            }
            if (!result.IsValid)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }
            model = await _mediator.Send(new UpdateAuthorCommand() { Model = model });
            return Ok(model);


        }
    }
}
