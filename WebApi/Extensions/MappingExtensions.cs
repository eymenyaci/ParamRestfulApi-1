using System.Collections.Generic;
using WebApi.Dto;
using WebApi.Dtos;
using WebApi.Models.Entity;

namespace WebApi.Extensions
{
    public static class MappingExtensions
    {
        #region Book mapping
        public static BookDto ToModel(this Book entity)
        {
            return entity.MapTo<Book, BookDto>();
        }

        public static Book ToEntity(this BookDto model)
        {
            return model.MapTo<BookDto, Book>();
        }

        public static List<BookDto> ToModel(this List<Book> model)
        {
            return model.MapTo<List<Book>, List<BookDto>>();
        }

        public static List<Book> ToEntity(this List<BookDto> model)
        {
            return model.MapTo<List<BookDto>, List<Book>>();
        }

        public static Book ToEntity(this BookDto model, Book destination)
        {
            return model.MapTo(destination);
        }
        #endregion

        #region Author mapping
        public static AuthorDto ToModel(this Author entity)
        {
            return entity.MapTo<Author, AuthorDto>();
        }

        public static Author ToEntity(this AuthorDto model)
        {
            return model.MapTo<AuthorDto, Author>();
        }

        public static List<AuthorDto> ToModel(this List<Author> model)
        {
            return model.MapTo<List<Author>, List<AuthorDto>>();
        }

        public static List<Author> ToEntity(this List<AuthorDto> model)
        {
            return model.MapTo<List<AuthorDto>, List<Author>>();
        }

        public static Author ToEntity(this AuthorDto model, Author destination)
        {
            return model.MapTo(destination);
        }
        #endregion

        #region Genre mapping
        public static GenreDto ToModel(this Genre entity)
        {
            return entity.MapTo<Genre, GenreDto>();
        }

        public static Genre ToEntity(this GenreDto model)
        {
            return model.MapTo<GenreDto, Genre>();
        }

        public static List<GenreDto> ToModel(this List<Genre> model)
        {
            return model.MapTo<List<Genre>, List<GenreDto>>();
        }

        public static List<Genre> ToEntity(this List<GenreDto> model)
        {
            return model.MapTo<List<GenreDto>, List<Genre>>();
        }

        public static Genre ToEntity(this GenreDto model, Genre destination)
        {
            return model.MapTo(destination);
        }
        #endregion

    }
}
