using BaratoInventory.Core.Models;
using BaratoInventory.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaratoInventory.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    // GET /api/v1/products?search=phone&sort=Price&order=asc
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll(
        string? search,
        string? sort = "Name",
        string? order = "asc")
    {
        var products = await _service.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(search))
            products = products
                .Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                .ToList();

        products = (sort, order?.ToLower()) switch
        {
            ("Name", "desc") => products.OrderByDescending(p => p.Name).ToList(),
            ("Category", "desc") => products.OrderByDescending(p => p.Category).ToList(),
            ("Price", "desc") => products.OrderByDescending(p => p.Price).ToList(),
            ("Quantity", "desc") => products.OrderByDescending(p => p.Quantity).ToList(),
            ("Name", _) => products.OrderBy(p => p.Name).ToList(),
            ("Category", _) => products.OrderBy(p => p.Category).ToList(),
            ("Price", _) => products.OrderBy(p => p.Price).ToList(),
            ("Quantity", _) => products.OrderBy(p => p.Quantity).ToList(),
            _ => products
        };

        return Ok(products);
    }

    // GET /api/v1/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    // POST /api/v1/products
    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _service.CreateAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/v1/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        if (id != product.Id) return BadRequest("ID mismatch");

        var updated = await _service.UpdateAsync(product);
        if (updated == null) return NotFound();

        return NoContent();
    }

    // DELETE /api/v1/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        await _service.DeleteAsync(id);
        return NoContent();
    }
}
