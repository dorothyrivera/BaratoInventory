using BaratoInventory.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BaratoInventory.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
        public DbSet<Product> Products { get; set; } = null!;
    }
}
