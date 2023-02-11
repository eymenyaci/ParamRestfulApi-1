using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entity
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }

    public class Log
    {
        public int Id { get; set; }
        public DateTime dateTime { get; set; }
        public string text { get; set; }
    }
}
