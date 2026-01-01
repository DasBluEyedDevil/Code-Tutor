using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopFlow.Application.Auth.DTOs;
using ShopFlow.Application.Carts.DTOs;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.ValueObjects;
using ShopFlow.Infrastructure.Persistence;

namespace ShopFlow.Tests.Integration;

/// <summary>
/// Custom WebApplicationFactory for Cart API tests.
/// Uses InMemory database instead of SQLite.
/// </summary>
public class CartApiTestFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = "CartTestDb_" + Guid.NewGuid().ToString();

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

public class CartApiTests : IClassFixture<CartApiTestFactory>, IAsyncLifetime, IDisposable
{
    private readonly CartApiTestFactory _factory;
    private readonly HttpClient _client;
    private readonly IServiceScope _scope;
    private readonly AppDbContext _context;
    private string _accessToken = string.Empty;

    public CartApiTests(CartApiTestFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _scope = _factory.Services.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
        _context.Database.EnsureCreated();
    }

    public async Task InitializeAsync()
    {
        // Register and authenticate a user for all tests
        var uniqueEmail = $"carttest_{Guid.NewGuid()}@example.com";
        var registerRequest = new
        {
            Email = uniqueEmail,
            Password = "Password123!",
            FirstName = "Cart",
            LastName = "Tester"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/register", registerRequest);
        response.EnsureSuccessStatusCode();
        var authResult = await response.Content.ReadFromJsonAsync<AuthResultDto>();
        _accessToken = authResult!.AccessToken;
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
    }

    public Task DisposeAsync() => Task.CompletedTask;

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

    private async Task<Product> SeedProductAsync(int categoryId, string name = "Test Product", int stock = 100)
    {
        var product = Product.Create(
            name,
            "Test Description",
            Money.Create(29.99m, "USD"),
            categoryId,
            stock
        );
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    #region GET /api/carts/{userId}

    [Fact]
    public async Task GetCart_ReturnsEmptyCart_WhenNoCartExists()
    {
        // Arrange
        var userId = "new-user-" + Guid.NewGuid().ToString();

        // Act
        var response = await _client.GetAsync($"/api/carts/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var cart = await response.Content.ReadFromJsonAsync<CartDto>();
        Assert.NotNull(cart);
        Assert.Equal(userId, cart.UserId);
        Assert.Empty(cart.Items);
        Assert.Equal(0, cart.TotalAmount);
    }

    [Fact]
    public async Task GetCart_ReturnsCartWithItems_WhenCartExists()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Cart Product " + Guid.NewGuid().ToString());

        var addRequest = new { ProductId = product.Id, Quantity = 2 };
        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", addRequest);

        // Act
        var response = await _client.GetAsync($"/api/carts/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var cart = await response.Content.ReadFromJsonAsync<CartDto>();
        Assert.NotNull(cart);
        Assert.Single(cart.Items);
        Assert.Equal(2, cart.Items.First().Quantity);
    }

    #endregion

    #region POST /api/carts/{userId}/items

    [Fact]
    public async Task AddToCart_CreatesCartAndAddsItem_WhenNoCartExists()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "New Cart Product " + Guid.NewGuid().ToString());

        var request = new { ProductId = product.Id, Quantity = 3 };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/carts/{userId}/items", request);

        // Assert
        response.EnsureSuccessStatusCode();
        var cart = await response.Content.ReadFromJsonAsync<CartDto>();
        Assert.NotNull(cart);
        Assert.Equal(userId, cart.UserId);
        Assert.Single(cart.Items);
        Assert.Equal(product.Id, cart.Items.First().ProductId);
        Assert.Equal(3, cart.Items.First().Quantity);
    }

    [Fact]
    public async Task AddToCart_UpdatesQuantity_WhenProductAlreadyInCart()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Duplicate Product " + Guid.NewGuid().ToString());

        var request1 = new { ProductId = product.Id, Quantity = 2 };
        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", request1);

        var request2 = new { ProductId = product.Id, Quantity = 3 };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/carts/{userId}/items", request2);

        // Assert
        response.EnsureSuccessStatusCode();
        var cart = await response.Content.ReadFromJsonAsync<CartDto>();
        Assert.NotNull(cart);
        Assert.Single(cart.Items);
        Assert.Equal(5, cart.Items.First().Quantity);
    }

    [Fact]
    public async Task AddToCart_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var request = new { ProductId = 99999, Quantity = 1 };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/carts/{userId}/items", request);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task AddToCart_ReturnsBadRequest_WhenInsufficientStock()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Low Stock Product " + Guid.NewGuid().ToString(), stock: 5);

        var request = new { ProductId = product.Id, Quantity = 10 };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/carts/{userId}/items", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion

    #region PUT /api/carts/{userId}/items/{productId}

    [Fact]
    public async Task UpdateCartItemQuantity_UpdatesQuantity_WhenItemExists()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Update Product " + Guid.NewGuid().ToString());

        var addRequest = new { ProductId = product.Id, Quantity = 2 };
        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", addRequest);

        var updateRequest = new { Quantity = 5 };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/carts/{userId}/items/{product.Id}", updateRequest);

        // Assert
        response.EnsureSuccessStatusCode();
        var cart = await response.Content.ReadFromJsonAsync<CartDto>();
        Assert.NotNull(cart);
        Assert.Equal(5, cart.Items.First().Quantity);
    }

    [Fact]
    public async Task UpdateCartItemQuantity_RemovesItem_WhenQuantityIsZero()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Remove Product " + Guid.NewGuid().ToString());

        var addRequest = new { ProductId = product.Id, Quantity = 2 };
        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", addRequest);

        var updateRequest = new { Quantity = 0 };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/carts/{userId}/items/{product.Id}", updateRequest);

        // Assert
        response.EnsureSuccessStatusCode();
        var cart = await response.Content.ReadFromJsonAsync<CartDto>();
        Assert.NotNull(cart);
        Assert.Empty(cart.Items);
    }

    [Fact]
    public async Task UpdateCartItemQuantity_ReturnsNotFound_WhenCartDoesNotExist()
    {
        // Arrange
        var userId = "nonexistent-user-" + Guid.NewGuid().ToString();
        var updateRequest = new { Quantity = 5 };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/carts/{userId}/items/1", updateRequest);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    #endregion

    #region DELETE /api/carts/{userId}/items/{productId}

    [Fact]
    public async Task RemoveFromCart_RemovesItem_WhenItemExists()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();
        var product1 = await SeedProductAsync(categoryId, "Product 1 " + Guid.NewGuid().ToString());
        var product2 = await SeedProductAsync(categoryId, "Product 2 " + Guid.NewGuid().ToString());

        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = product1.Id, Quantity = 2 });
        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = product2.Id, Quantity = 1 });

        // Act
        var response = await _client.DeleteAsync($"/api/carts/{userId}/items/{product1.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var cart = await response.Content.ReadFromJsonAsync<CartDto>();
        Assert.NotNull(cart);
        Assert.Single(cart.Items);
        Assert.Equal(product2.Id, cart.Items.First().ProductId);
    }

    [Fact]
    public async Task RemoveFromCart_ReturnsNotFound_WhenCartDoesNotExist()
    {
        // Arrange
        var userId = "nonexistent-user-" + Guid.NewGuid().ToString();

        // Act
        var response = await _client.DeleteAsync($"/api/carts/{userId}/items/1");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task RemoveFromCart_ReturnsBadRequest_WhenItemNotInCart()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Cart Product " + Guid.NewGuid().ToString());

        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = product.Id, Quantity = 2 });

        // Act
        var response = await _client.DeleteAsync($"/api/carts/{userId}/items/99999");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion

    #region DELETE /api/carts/{userId}

    [Fact]
    public async Task ClearCart_RemovesAllItems_WhenCartExists()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();
        var product1 = await SeedProductAsync(categoryId, "Clear Product 1 " + Guid.NewGuid().ToString());
        var product2 = await SeedProductAsync(categoryId, "Clear Product 2 " + Guid.NewGuid().ToString());

        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = product1.Id, Quantity = 2 });
        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = product2.Id, Quantity = 1 });

        // Act
        var response = await _client.DeleteAsync($"/api/carts/{userId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Verify cart is empty
        var getResponse = await _client.GetAsync($"/api/carts/{userId}");
        var cart = await getResponse.Content.ReadFromJsonAsync<CartDto>();
        Assert.Empty(cart!.Items);
    }

    [Fact]
    public async Task ClearCart_ReturnsNotFound_WhenCartDoesNotExist()
    {
        // Arrange
        var userId = "nonexistent-user-" + Guid.NewGuid().ToString();

        // Act
        var response = await _client.DeleteAsync($"/api/carts/{userId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    #endregion

    #region Cart Total Calculation

    [Fact]
    public async Task GetCart_CalculatesTotalCorrectly()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();

        var product1 = Product.Create("Product 1", "Desc", Money.Create(10.00m, "USD"), categoryId, 100);
        var product2 = Product.Create("Product 2", "Desc", Money.Create(5.00m, "USD"), categoryId, 100);
        _context.Products.AddRange(product1, product2);
        await _context.SaveChangesAsync();

        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = product1.Id, Quantity = 2 }); // 20.00
        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = product2.Id, Quantity = 3 }); // 15.00

        // Act
        var response = await _client.GetAsync($"/api/carts/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var cart = await response.Content.ReadFromJsonAsync<CartDto>();
        Assert.NotNull(cart);
        Assert.Equal(35.00m, cart.TotalAmount);
        Assert.Equal(5, cart.ItemCount);
    }

    #endregion
}
