using Dapper;
using EfCeeEmSharp.Thread.Contracts;
using EfCeeEmSharp.Thread.Data;

namespace EfCeeEmSharp.Thread.Domain;

public class ThreadQueryService : IThreadQueryService
{
    private readonly ThreadDbContext _dbContext;

    public ThreadQueryService(ThreadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Models.Thread>> GetThreadsAsync()
    {
        return await _dbContext.Connection.QueryAsync<Models.Thread>("select * from threads");
    }

    public async Task<Models.Thread?> GetMostRecentThreadAsync(string board)
    {
        return await _dbContext.Connection.QueryFirstOrDefaultAsync<Models.Thread?>(
            "select * from Threads where Board = @Board order by LastModifiedAt desc", new { Board = board });
    }
}