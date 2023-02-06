using System;

namespace WebApi
{
    public class Book : AutoID
    {
        public string BookName { get; set; }

        public string Author { get; set; }
        public int PageCount { get; set; }
    }

    public class Log : AutoID
    {
        public DateTime dateTime { get; set; }
        public string text { get; set; }
    }
}
