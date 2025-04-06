using AiPlayground.DataAccess.Configurations;
using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiPlayground.DataAccess;

public class AiPlaygroundContext : DbContext
{
    public AiPlaygroundContext(DbContextOptions<AiPlaygroundContext> options) : base(options)
    {
        
    }

    public DbSet<Platform> Platforms { get; set; } = null!;
    public DbSet<Model> Models { get; set; } = null!;
    public DbSet<Run> Run { get; set; } = null!;
    public DbSet<Scope> Scope { get; set; } = null!;
    public DbSet<Prompt> Prompt { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        new PlatformConfiguration().Configure(modelBuilder.Entity<Platform>());
        new ModelConfiguration().Configure(modelBuilder.Entity<Model>());
        new RunConfiguration().Configure(modelBuilder.Entity<Run>());
        new ScopeConfiguration().Configure(modelBuilder.Entity<Scope>());
        new PromptConfiguration().Configure(modelBuilder.Entity<Prompt>());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=master;User Id=sa;Password=0.-Remy-.0;Encrypt=True;TrustServerCertificate=True;");
    }
}