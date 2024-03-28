using Microsoft.EntityFrameworkCore;
using Glimpse.Models;

namespace Glimpse.Migrations;

public class GlimpseContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=GlimpseDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
    }
}
