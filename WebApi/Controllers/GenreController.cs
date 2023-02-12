using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Commands.Model.Book;
using WebApi.Commands.Model.Genre;
using WebApi.Dto;
using WebApi.Dtos;
using WebApi.Validations.Book;
using WebApi.Validations.Genre;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerOperation(summary: "Get entities from Genre", OperationId = "GetGenres")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var genreList = await _mediator.Send(new GetGenreListCommand());
            //Is any book ?
            if (!genreList.Any())
            {
                return BadRequest();
            }
            return Ok(genreList);

        }

        [SwaggerOperation(summary: "Get entity from Genre by id", OperationId = "GetGenreById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // genre get by id
            var genre = await _mediator.Send(new GetGenreCommand { Id = id });
            if (genre is null && id != 0)
            {
                return NotFound(id);
            }
            
            if (id == 0)
            {
                return BadRequest("Id cannot be 0.");
            }

            return Ok(genre);
        }

        [SwaggerOperation(summary: "Delete entity from Genre", OperationId = "DeleteGenre")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var genre = await _mediator.Send(new GetGenreCommand { Id = id });
            if (genre is null && id != 0)
            {
                return NotFound();
            }
            if (id == 0)
            {
                return BadRequest("Id cannot be 0.");
            }

            var book = await _mediator.Send(new GetBookCommand { Id = id ,IdType = "Genre"});
            if (book is not null)
            {
                return BadRequest("The genre cannot be deleted as they have books associated with them");
            }
            
            await _mediator.Send(new DeleteGenreCommand() { Model = genre });
            return Ok();
        }

        [SwaggerOperation(summary: "Add new entity to Genre", OperationId = "InsertGenre")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GenreDto model)
        {
            var validator = new AddGenreValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }
            model = await _mediator.Send(new AddGenreCommand() { Model = model });
            return Ok(model);

        }

        [SwaggerOperation(summary: "Update entity in Genre", OperationId = "UpdateGenre")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GenreDto model)
        {
            var validator = new UpdateGenreValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }

            var genre = await _mediator.Send(new GetGenreCommand() { Id = model.Id });
            if (genre is null)
            {
                return NotFound();
            }

            
            model = await _mediator.Send(new UpdateGenreCommand() { Model = model });
            return Ok(model);


        }
    }
}
