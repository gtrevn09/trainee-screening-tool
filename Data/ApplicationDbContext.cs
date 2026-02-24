using Microsoft.EntityFrameworkCore;
using TraineeScreeningTool.Models;

namespace TraineeScreeningTool.Data;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    public DbSet<Candidate> Candidates { get; set; }
}