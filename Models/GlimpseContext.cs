using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Glimpse.Models;
public class GlimpseContext : IdentityDbContext<User>
{
    public GlimpseContext(DbContextOptions<GlimpseContext> options) : base(options)
    {

    }
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Board> Boards { get; set; } = null!;
    public DbSet<Lane> Lanes { get; set; } = null!;
    public DbSet<Card> Cards { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Checkbox> Checkboxes { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=GlimpseDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        });
        /*modelBuilder.Entity<Board>()
            .HasIndex(a => a.Id)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasMany(a => a.Projects)
            .WithMany(a => a.Users)
            .UsingEntity(
                "UserProject",
                l => l.HasOne(typeof(Project)).WithMany().HasForeignKey("ProjectsId").HasPrincipalKey(nameof(Project.Id)),
                r => r.HasOne(typeof(User)).WithMany().HasForeignKey("UsersId").HasPrincipalKey(nameof(User.Id)),
                j => j.HasKey("UsersId", "ProjectsId"));*/

        // N x N -> User e Card
        modelBuilder.Entity<User>()
            .HasMany(a => a.Cards)
            .WithMany(a => a.Users)
            .UsingEntity(
                "UserCard",
                l => l.HasOne(typeof(Card)).WithMany().HasForeignKey("CardsId").HasPrincipalKey(nameof(Card.Id)),
                r => r.HasOne(typeof(User)).WithMany().HasForeignKey("UsersId").HasPrincipalKey(nameof(User.Id)),
                j => j.HasKey("UsersId", "CardsId"));

        // N x N -> Card e Tag
        modelBuilder.Entity<Card>()
            .HasMany(a => a.Tags)
            .WithMany(a => a.Cards)
            .UsingEntity(
                "CardTag",
                l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagsId").HasPrincipalKey(nameof(Tag.Id)),
                r => r.HasOne(typeof(Card)).WithMany().HasForeignKey("CardsId").HasPrincipalKey(nameof(Card.Id)),
                j => j.HasKey("CardsId", "TagsId"));
        
        // 1 x N -> Card e Checkbox
        modelBuilder.Entity<Card>()
            .HasMany(e => e.Checkboxes)
            .WithOne(e => e.Card)
            .HasForeignKey("CardId")
            .IsRequired(false);

        modelBuilder.Entity<Project>()
            .HasMany(e => e.Boards)
            .WithOne(e => e.Project)
            .HasForeignKey("ProjectId")
            .IsRequired(false);
    }
}