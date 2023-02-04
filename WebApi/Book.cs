using System;

namespace WebApi
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }

        public string Author { get; set; }
        public int PageCount { get; set; }
    }

    public class Log
    {
        public DateTime dateTime { get; set; }
        public string text { get; set; }
    }
}
