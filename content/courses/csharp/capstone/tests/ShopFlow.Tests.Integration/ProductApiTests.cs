using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopFlow.Application.Products.DTOs;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.ValueObjects;
using ShopFlow.Infrastructure.Persistence;

namespace ShopFlow.Tests.Integration;

/// <summary>
/// Custom WebApplicationFactory for Product API tests.
/// Uses InMemory database instead of SQLite.
/// </summary>
public class ProductApiTestFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = "TestDb_" + Guid.NewGuid().ToString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove ALL EF Core related registrations
            var descriptorsToRemove = services
                .Where(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>) ||
                            d.ServiceType == typeof(DbContextOptions) ||
                            d.ServiceType == typeof(AppDbContext) ||
                            d.ServiceType.FullName?.Contains("EntityFrameworkCore") == true ||
                            d.ServiceType.FullName?.Contains("DbContext") == true)
                .ToList();

            foreach (var descriptor in descriptorsToRemove)
            {
                services.Remove(descriptor);
            }

            // Add InMemory database
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(_dbName);
            });
        });
    }
}

public class ProductApiTests : IClassFixture<ProductApiTestFactory>, IDisposable
{
    private readonly ProductApiTestFactory _factory;
    private readonly HttpClient _client;
    private readonly IServiceScope _scope;
    private readonly AppDbContext _context;

    public ProductApiTests(ProductApiTestFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _scope = _factory.Services.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
        _context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _client.Dispose();
        _scope.Dispose();
    }

    private async Task<int> SeedCategoryAsync()
    {
        var category = Category.Create("Test Category", "Test Description");
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category.Id;
    }

    private async Task<Product> SeedProductAsync(int categoryId, string name = "Test Product")
    {
        var product = Product.Create(
            name,
            "Test Description",
            Money.Create(29.99m, "USD"),
            categoryId,
            100
        );
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    #region GET /api/products

    [Fact]
    public async Task GetProducts_ReturnsEmptyList_WhenNoProducts()
    {
        // Act
        var response = await _client.GetAsync("/api/products");

        // Assert
        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        Assert.NotNull(products);
    }

    [Fact]
    public async Task GetProducts_ReturnsProducts_WhenProductsExist()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();
        await SeedProductAsync(categoryId);

        // Act
        var response = await _client.GetAsync("/api/products");

        // Assert
        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        Assert.NotNull(products);
        Assert.NotEmpty(products);
    }

    #endregion

    #region GET /api/products/{id}

    [Fact]
    public async Task GetProductById_ReturnsProduct_WhenExists()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId);

        // Act
        var response = await _client.GetAsync($"/api/products/{product.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ProductDto>();
        Assert.NotNull(result);
        Assert.Equal(product.Name, result.Name);
    }

    [Fact]
    public async Task GetProductById_ReturnsNotFound_WhenDoesNotExist()
    {
        // Act
        var response = await _client.GetAsync("/api/products/99999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    #endregion

    #region POST /api/products

    [Fact]
    public async Task CreateProduct_ReturnsCreated_WithValidData()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();
        var request = new
        {
            Name = "New Product " + Guid.NewGuid().ToString(),
            Description = "New Description",
            Price = 49.99m,
            Currency = "USD",
            CategoryId = categoryId,
            StockQuantity = 50
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/products", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ProductDto>();
        Assert.NotNull(result);
        Assert.StartsWith("New Product", result.Name);
        Assert.Equal(49.99m, result.Price);
    }

    [Fact]
    public async Task CreateProduct_ReturnsBadRequest_WhenCategoryNotFound()
    {
        // Arrange
        var request = new
        {
            Name = "New Product",
            Description = "New Description",
            Price = 49.99m,
            Currency = "USD",
            CategoryId = 99999,
            StockQuantity = 50
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/products", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion

    #region PUT /api/products/{id}

    [Fact]
    public async Task UpdateProduct_ReturnsOk_WithValidData()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId);

        var request = new
        {
            Name = "Updated Product " + Guid.NewGuid().ToString(),
            Description = "Updated Description",
            Price = 59.99m,
            Currency = "USD",
            CategoryId = categoryId
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/products/{product.Id}", request);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ProductDto>();
        Assert.NotNull(result);
        Assert.StartsWith("Updated Product", result.Name);
        Assert.Equal(59.99m, result.Price);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsNotFound_WhenDoesNotExist()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();
        var request = new
        {
            Name = "Updated Product",
            Description = "Updated Description",
            Price = 59.99m,
            Currency = "USD",
            CategoryId = categoryId
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/products/99999", request);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    #endregion

    #region DELETE /api/products/{id}

    [Fact]
    public async Task DeleteProduct_ReturnsNoContent_WhenExists()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Product To Delete " + Guid.NewGuid().ToString());

        // Act
        var response = await _client.DeleteAsync($"/api/products/{product.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Verify product is deleted
        var getResponse = await _client.GetAsync($"/api/products/{product.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNotFound_WhenDoesNotExist()
    {
        // Act
        var response = await _client.DeleteAsync("/api/products/99999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    #endregion

    #region POST /api/products/{id}/deactivate and /activate

    [Fact]
    public async Task DeactivateProduct_ReturnsNoContent_WhenExists()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Product To Deactivate " + Guid.NewGuid().ToString());

        // Act
        var response = await _client.PostAsync($"/api/products/{product.Id}/deactivate", null);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Verify product is deactivated
        var getResponse = await _client.GetAsync($"/api/products/{product.Id}");
        getResponse.EnsureSuccessStatusCode();
        var result = await getResponse.Content.ReadFromJsonAsync<ProductDto>();
        Assert.NotNull(result);
        Assert.False(result.IsActive);
    }

    [Fact]
    public async Task ActivateProduct_ReturnsNoContent_WhenExists()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Product To Activate " + Guid.NewGuid().ToString());
        product.Deactivate();
        await _context.SaveChangesAsync();

        // Act
        var response = await _client.PostAsync($"/api/products/{product.Id}/activate", null);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Verify product is activated
        var getResponse = await _client.GetAsync($"/api/products/{product.Id}");
        getResponse.EnsureSuccessStatusCode();
        var result = await getResponse.Content.ReadFromJsonAsync<ProductDto>();
        Assert.NotNull(result);
        Assert.True(result.IsActive);
    }

    #endregion

    #region POST /api/products/{id}/stock

    [Fact]
    public async Task UpdateStock_AddStock_ReturnsNoContent()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Stock Product " + Guid.NewGuid().ToString());
        var initialStock = product.StockQuantity;

        var request = new { Quantity = 10, IsAddition = true };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/products/{product.Id}/stock", request);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Verify stock is updated
        var getResponse = await _client.GetAsync($"/api/products/{product.Id}");
        var result = await getResponse.Content.ReadFromJsonAsync<ProductDto>();
        Assert.Equal(initialStock + 10, result!.StockQuantity);
    }

    [Fact]
    public async Task UpdateStock_RemoveStock_ReturnsNoContent()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Stock Remove Product " + Guid.NewGuid().ToString());
        var initialStock = product.StockQuantity;

        var request = new { Quantity = 10, IsAddition = false };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/products/{product.Id}/stock", request);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Verify stock is updated
        var getResponse = await _client.GetAsync($"/api/products/{product.Id}");
        var result = await getResponse.Content.ReadFromJsonAsync<ProductDto>();
        Assert.Equal(initialStock - 10, result!.StockQuantity);
    }

    [Fact]
    public async Task UpdateStock_RemoveMoreThanAvailable_ReturnsBadRequest()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Overstock Product " + Guid.NewGuid().ToString());

        var request = new { Quantity = product.StockQuantity + 100, IsAddition = false };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/products/{product.Id}/stock", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion
}
