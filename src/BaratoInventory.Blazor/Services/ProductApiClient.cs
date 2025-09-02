using BaratoInventory.Core.DTOs;

namespace BaratoInventory.Blazor.Services
{
    public class ProductApiClient
    {
        private readonly HttpClient _http;

        public ProductApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProductDto>> GetAllAsync(string search = "", string sort = "Name", string order = "asc")
        {
            var url = $"api/v1/products?search={search}&sort={sort}&order={order}";
            return await _http.GetFromJsonAsync<List<ProductDto>>(url) ?? new List<ProductDto>();
        }

        public async Task<ProductDto?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<ProductDto>($"api/v1/products/{id}");

        public async Task<ProductDto?> CreateAsync(ProductDto product)
        {
            var response = await _http.PostAsJsonAsync("api/v1/products", product);
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<bool> UpdateAsync(ProductDto product)
        {
            var response = await _http.PutAsJsonAsync($"api/v1/products/{product.Id}", product);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/products/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
