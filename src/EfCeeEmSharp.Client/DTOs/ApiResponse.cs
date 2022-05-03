namespace EfCeeEmSharp.Client.DTOs;

public class ApiResponse<T>
{
    public T Data { get; set; } = default!;
    public MetaData Meta { get; set; } = null!;

    public class MetaData
    {
        public string? ETag { get; set; } = null!;
        public DateTimeOffset? LastModifiedAt { get; set; }
    }
}