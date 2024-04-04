using Microsoft.EntityFrameworkCore;

namespace Glimpse.Models
{
    public class GlimpseContext : DbContext
    {
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Board> Boards { get; set; } = null!;
        
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