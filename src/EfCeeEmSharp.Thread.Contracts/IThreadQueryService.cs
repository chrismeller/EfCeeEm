namespace EfCeeEmSharp.Thread.Contracts;

public interface IThreadQueryService
{
    public Task<IEnumerable<Models.Thread>> GetThreadsAsync();
    public Task<Models.Thread?> GetMostRecentThreadAsync(string board);
}