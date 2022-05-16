using System.Data;
using Microsoft.EntityFrameworkCore;

namespace EfCeeEmSharp.Thread.Data;

public class ThreadDbContext : DbContext
{
    public ThreadDbContext(DbContextOptions<ThreadDbContext> options): base(options) {}

    public DbSet<Models.Thread>? Threads { get; set; }


    public IDbConnection Connection => Database.GetDbConnection();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Models.Thread>()
            .HasKey(t => new { t.Board, t.Number });

        // @todo make this IsDescending when that feature gets released
        modelBuilder.Entity<Models.Thread>()
            .HasIndex(t => t.LastModifiedAt);
    }
}