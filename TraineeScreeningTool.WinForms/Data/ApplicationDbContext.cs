using Microsoft.EntityFrameworkCore;
using TraineeScreeningTool.WinForms.Models;

namespace TraineeScreeningTool.WinForms.Data;

// Manages the connection between the app and the SQLite database
public class ApplicationDbContext : DbContext
{
    // Represents the Candidates table in the database
    public DbSet<Candidate> Candidates { get; set; }

    // Represents the Users table in the database
    public DbSet<User> Users { get; set; }

    // Represents the AppLogs table in the database
    public DbSet<AppLog> AppLogs { get; set; }

    // Configures the database to use SQLite and sets the file location
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Gets the folder where the .exe is running from and puts the db there
        var folder = AppDomain.CurrentDomain.BaseDirectory;
        var dbPath = Path.Combine(folder, "trainees.db");
        options.UseSqlite($"Data Source={dbPath}");
    }

    // Seeds the database with a default admin account on first run
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "admin",
            // Default password is "lifeworks123" - hashed for security
            PasswordHash = HashPassword("lifeworks123"),
            Role = "Admin",
            IsActive = true
        });
    }

    // Simple password hashing using SHA256
    public static string HashPassword(string password)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    // Logs an action to the database
    public void Log(string username, string action, string details, bool success = true)
    {
        AppLogs.Add(new AppLog
        {
            Username = username,
            Action = action,
            Details = details,
            Timestamp = DateTime.Now,
            Success = success
        });
        SaveChanges(); // Save the log entry immediately
    }
}