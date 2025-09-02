using BaratoInventory.Core.Models;

namespace BaratoInventory.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any()) return;

            var products = new[]
            {
                new Product { Name = "Laptop", Category = "Electronics", Price = 1200m, Quantity = 10 },
                new Product { Name = "Smartphone", Category = "Electronics", Price = 800m, Quantity = 25 },
                new Product { Name = "Headphones", Category = "Accessories", Price = 200m, Quantity = 50 },
                new Product { Name = "Monitor", Category = "Electronics", Price = 150m, Quantity = 15 },
                new Product { Name = "Office Chair", Category = "Furniture", Price = 300m, Quantity = 5 }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
