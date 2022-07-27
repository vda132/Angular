using Microsoft.EntityFrameworkCore;

namespace DBLayer.Context;

public class TasksContext : DbContext
{
    public TasksContext(DbContextOptions<TasksContext> options) : base(options)
    {
    }

    public DbSet<DBLayer.Models.Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
