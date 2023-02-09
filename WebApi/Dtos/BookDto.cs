
namespace WebApi.Dto
{
    public class BookDto : AutoID
    {        
        public string BookName { get; set; }   
        public string Author { get; set; }
        public int PageCount { get; set; }

    }

}
