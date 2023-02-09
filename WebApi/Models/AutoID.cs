using System.ComponentModel.DataAnnotations.Schema;
public class AutoID
{
    private static int nextID = 1;

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public AutoID()
    {
        Id = nextID++;
    }
}