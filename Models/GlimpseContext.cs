using Microsoft.EntityFrameworkCore;



namespace Glimpse.Models;

    public class GlimpseContext : DbContext
    {
        public DbSet<List> Lists { get; set; } = null!;
        
        // teste de merge

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=GlimpseDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        }
    }
