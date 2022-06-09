namespace EfCeeEmSharp.Thread.Contracts;

public interface IThreadWriteService
{
    public Task UpdateThreadMeta(string board, long thread, string etag, DateTimeOffset lastModifiedAt);
}