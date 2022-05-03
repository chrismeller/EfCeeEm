using Microsoft.EntityFrameworkCore;

namespace EfCeeEmSharp.Thread.Data;

public class ThreadDbContext : DbContext
{
    public ThreadDbContext(DbContextOptions<ThreadDbContext> options): base(options) {}
    
    public DbSet<Models.Thread>? Threads { get; set; }
}