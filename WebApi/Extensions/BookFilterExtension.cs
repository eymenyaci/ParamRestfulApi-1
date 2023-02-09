//using System.Collections.Generic;
//using System.Linq;
//using WebApi.Models.Entity;

//namespace WebApi.Extensions
//{
//    public static class BookFilterExtension
//    {
//        //Extension filtered by book name
//        public static IEnumerable<Book> FilterByAuthor(this IEnumerable<Book> books, string author)
//        {
//            return books.Where(x=> x.Author == author);    
//        }
//        //Extension filtered by author name
//        public static IEnumerable<Book> FilterByBookName(this IEnumerable<Book> books, string bookName)
//        {
//            return books.Where(x => x.BookName == bookName);
//        }
//    }
//}
