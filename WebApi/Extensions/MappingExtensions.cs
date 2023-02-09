using System.Collections.Generic;
using WebApi.Dto;
using WebApi.Models.Entity;

namespace WebApi.Extensions
{
    public static class MappingExtensions
    {
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
    }
}
