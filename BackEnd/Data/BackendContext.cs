using Microsoft.EntityFrameworkCore;

public class BackendContext : DbContext
{
    public DbSet<Assignment> Assignments { get; set; } = null!;
    public DbSet<Holiday> Holidays { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;


    public string DbPath { get; private set; }

    public BackendContext(DbContextOptions<BackendContext> options) : base(options)
    {
        DbPath = "Backend.db";
    }

    public BackendContext()
    {
        // Path to SQLite database file
        DbPath = "Backend.db";
    }


    // The following configures EF to create a SQLite database file locally
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Use SQLite as database
        options.UseSqlite($"Data Source={DbPath}");
        // Optional: log SQL queries to console
        // options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
    }
}
