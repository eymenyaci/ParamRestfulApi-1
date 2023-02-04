public class AutoID
{
    private static int nextID = 1;

    public int Id { get; set; }

    public AutoID()
    {
        Id = nextID++;
    }
}