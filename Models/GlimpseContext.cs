using Microsoft.EntityFrameworkCore;

namespace Glimpse.Models
{
    public class GlimpseContext : DbContext
    {
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Board> Boards { get; set; } = null!;
        public DbSet<Lane> Lanes { get; set; } = null!;
        public DbSet<Card> Cards { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Checkbox> Checkboxes { get; set; } = null!;
        public DbSet<Tag> Tag { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<UserCard> UserCards { get; set; } = null!;
        public DbSet<CardTag> CardTags { get; set; } = null!;
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=BRCTAW10431533\SQLEXPRESS;Database=GlimpseDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasIndex(a => a.ProjectId)
                .IsUnique();

            modelBuilder.Entity<Board>()
                .HasIndex(a => a.BoardId)
                .IsUnique();
        }
    }
}