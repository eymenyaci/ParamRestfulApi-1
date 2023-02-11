namespace WebApi.Dtos
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
