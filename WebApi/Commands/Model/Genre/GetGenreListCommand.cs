using MediatR;
using System.Collections.Generic;
using WebApi.Dtos;

namespace WebApi.Commands.Model.Genre
{
    public class GetGenreListCommand : IRequest<List<GenreDto>>
    {
    }
}
