using Microsoft.EntityFrameworkCore;
using Glimpse.Models;

namespace Glimpse.Migrations;

public class GlimpseContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Lane> Lanes { get; set; } = null!;
    public DbSet<Card> Cards { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=GlimpseDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasOne(t => t.ResponsibleUser)
            .WithMany()
            .HasForeignKey(t => t.ResponsibleUserId)
            .IsRequired(false);

        modelBuilder.Entity<Team>()
            .HasKey(t => new { t.FkUsersUserId, t.FkProjectsProjectId });

        modelBuilder.Entity<Team>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.FkUsersUserId);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.Project)
            .WithMany()
            .HasForeignKey(t => t.FkProjectsProjectId);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.Role)
            .WithMany()
            .HasForeignKey(t => t.FkRolesRoleId)
            .IsRequired(false);
    }
}
