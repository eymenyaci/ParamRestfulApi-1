using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi
{
    public class Book : AutoID
    {
        public string BookName { get; set; }

        public string Author { get; set; }
        public int PageCount { get; set; }
    }

    public class Log 
    {
        // public int LogId { get; set; }
        public int Id { get; set; }
        public DateTime dateTime { get; set; }
        public string text { get; set; }
    }
}
