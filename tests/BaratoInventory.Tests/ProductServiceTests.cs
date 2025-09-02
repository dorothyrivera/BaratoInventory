using BaratoInventory.Core.Models;
using BaratoInventory.Core.Repositories;
using BaratoInventory.Infrastructure.Services;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Text;
using System.Text.Json;

namespace BaratoInventory.Tests
{
    public class ProductServiceTests
    {
        [Test]
        public async Task GetAllAsync_ReturnsCached_WhenCacheHasValue()
        {
            var mockRepo = new Mock<IProductRepository>();
            var mockCache = new Mock<IDistributedCache>();

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "P" }
            };
            var json = JsonSerializer.Serialize(products);
            var bytes = Encoding.UTF8.GetBytes(json);

            mockCache
                .Setup(c => c.GetAsync("products:all", It.IsAny<CancellationToken>()))
                .ReturnsAsync(bytes);

            var service = new ProductService(mockRepo.Object, mockCache.Object);

            var result = await service.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            mockRepo.Verify(r => r.GetAllAsync(), Times.Never);
        }
    }
}
