using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Glimpse.Models;

public class GlimpseContext : IdentityDbContext<User>
{

    public GlimpseContext(DbContextOptions<GlimpseContext> options) : base(options)
    {

    }

    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Lane> Lanes { get; set; } = null!;
    public DbSet<Card> Cards { get; set; } = null!;
    public DbSet<List> Lists { get; set; } = null!;

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=GlimpseDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.ResponsibleUserId)
            .IsRequired(false);

        modelBuilder.Entity<Team>()
            .HasKey(t => new { t.FkUsers, t.FkProjects });

        modelBuilder.Entity<Team>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.FkUsers);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.Project)
            .WithMany()
            .HasForeignKey(t => t.FkProjects);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.Role)
            .WithMany()
            .HasForeignKey(t => t.FkRoles)
            .IsRequired(false);
    }
}
