using BaratoInventory.Core.Models;
using BaratoInventory.Core.Repositories;
using BaratoInventory.Core.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BaratoInventory.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IDistributedCache _cache;

        private const string ALL_KEY = "products:all";

        public ProductService(IProductRepository repo, IDistributedCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var cached = await _cache.GetStringAsync(ALL_KEY);
            if (!string.IsNullOrEmpty(cached))
                return JsonSerializer.Deserialize<List<Product>>(cached)!;

            var products = await _repo.GetAllAsync();

            await _cache.SetStringAsync(
                ALL_KEY,
                JsonSerializer.Serialize(products),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });

            return products;
        }

        public async Task<Product?> GetByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task<Product> CreateAsync(Product product)
        {
            var created = await _repo.AddAsync(product);
            await InvalidateCache();
            return created;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var updated = await _repo.UpdateAsync(product);
            await InvalidateCache();
            return updated;
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            await InvalidateCache();
        }

        private async Task InvalidateCache() =>
            await _cache.RemoveAsync(ALL_KEY);
    }
}
