using System.Text.Json.Serialization;

namespace EfCeeEmSharp.Client.DTOs;

public class ThreadPage
{
    public int Page { get; set; }
    public IEnumerable<ThreadPageThread> Threads { get; set; } = null!;
}

public class ThreadPageThread
{
    [JsonPropertyName("no")]
    public long Number { get; set; }

    [JsonPropertyName("last_modified")]
    public long LastModified { get; set; }

    public DateTimeOffset LastModifiedAt => DateTimeOffset.FromUnixTimeSeconds(LastModified);

    [JsonPropertyName("replies")]
    public int Replies { get; set; }
}