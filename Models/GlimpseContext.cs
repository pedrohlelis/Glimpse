using Microsoft.EntityFrameworkCore;
using Glimpse.Models;

namespace Glimpse.Migrations;

public class GlimpseContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=GlimpseDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Team>()
    //     .HasMany(e => e.Tags)
    //     .WithMany(e => e.Posts)
    //     .UsingEntity(
    //         l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagForeignKey"),
    //         r => r.HasOne(typeof(Post)).WithMany().HasForeignKey("PostForeignKey"));
    // }
}
