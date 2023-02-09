using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entity
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
    }

    public class Log
    {
        public int Id { get; set; }
        public DateTime dateTime { get; set; }
        public string text { get; set; }
    }
}
