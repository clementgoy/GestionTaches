using Microsoft.EntityFrameworkCore;

public class BackendContext : DbContext
{
    public DbSet<Assignment> Assignments { get; set; } = null!;
    public DbSet<Holiday> Holidays { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;

    public string DbPath { get; private set; }

    public BackendContext(DbContextOptions<BackendContext> options)
        : base(options)
    {
        DbPath = "Backend.db";
    }

    public BackendContext()
    {
        DbPath = "Backend.db";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
            options.UseSqlite($"Data Source={DbPath}");
    }
}
