using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


public class BackendContext : DbContext
{
    public DbSet<Assignation> Assignations { get; set; } = null!;
    public DbSet<Conge> Conges { get; set; } = null!;
    public DbSet<Employe> Employes { get; set; } = null!;
    public DbSet<Tache> Taches { get; set; } = null!;


    public string DbPath { get; private set; }


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
