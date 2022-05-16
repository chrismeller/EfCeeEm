namespace EfCeeEmSharp.Thread.Models;

public class Thread
{
    public string Board { get; set; } = null!;
    public long Number { get; set; }

    public string? ETag { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
}