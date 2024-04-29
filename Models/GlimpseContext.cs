using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace Glimpse.Models;
public class GlimpseContext(DbContextOptions<GlimpseContext> options) : IdentityDbContext<User>(options)
{
    // public GlimpseContext(DbContextOptions<GlimpseContext> options) : base(options)
    // {

    // }
    public virtual DbSet<Project> Projects { get; set; } = null!;
    public virtual DbSet<Board> Boards { get; set; } = null!;
    public virtual DbSet<Lane> Lanes { get; set; } = null!;
    public virtual DbSet<Card> Cards { get; set; } = null!;
    public virtual DbSet<Role> Roles { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Checkbox> Checkboxes { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=GlimpseDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
    // }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        });
    }
}