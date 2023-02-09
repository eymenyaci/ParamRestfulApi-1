using AutoMapper;
using WebApi.Dto;
using WebApi.Models.Entity;

namespace WebApi.Mapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookDto,Book>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id));
            CreateMap<Book, BookDto>();
        }
    }
}
