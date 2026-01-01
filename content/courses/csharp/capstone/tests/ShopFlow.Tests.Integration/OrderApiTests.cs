using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopFlow.Application.Auth.DTOs;
using ShopFlow.Application.Orders.DTOs;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Enums;
using ShopFlow.Domain.ValueObjects;
using ShopFlow.Infrastructure.Persistence;

namespace ShopFlow.Tests.Integration;

/// <summary>
/// Custom WebApplicationFactory for Order API tests.
/// Uses InMemory database instead of SQLite.
/// </summary>
public class OrderApiTestFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = "OrderTestDb_" + Guid.NewGuid().ToString();

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

public class OrderApiTests : IClassFixture<OrderApiTestFactory>, IAsyncLifetime, IDisposable
{
    private readonly OrderApiTestFactory _factory;
    private readonly HttpClient _client;
    private readonly IServiceScope _scope;
    private readonly AppDbContext _context;
    private string _accessToken = string.Empty;

    public OrderApiTests(OrderApiTestFactory factory)
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
        var uniqueEmail = $"ordertest_{Guid.NewGuid()}@example.com";
        var registerRequest = new
        {
            Email = uniqueEmail,
            Password = "Password123!",
            FirstName = "Order",
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

    private async Task<string> SetupCartWithItems(string? userId = null)
    {
        userId ??= "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Order Product " + Guid.NewGuid().ToString());

        var addRequest = new { ProductId = product.Id, Quantity = 2 };
        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", addRequest);

        return userId;
    }

    #region POST /api/orders - Create Order

    [Fact]
    public async Task CreateOrder_WithValidCart_CreatesOrderAndClearsCart()
    {
        // Arrange
        var userId = await SetupCartWithItems();
        var request = new { UserId = userId, ShippingAddress = "123 Main St" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/orders", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var order = await response.Content.ReadFromJsonAsync<OrderDto>();
        Assert.NotNull(order);
        Assert.Equal(userId, order.UserId);
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Equal("123 Main St", order.ShippingAddress);
        Assert.Single(order.Items);
        Assert.True(order.CanBeCancelled);

        // Verify cart is cleared
        var cartResponse = await _client.GetAsync($"/api/carts/{userId}");
        var cart = await cartResponse.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(cart);
    }

    [Fact]
    public async Task CreateOrder_WithNoCart_ReturnsNotFound()
    {
        // Arrange
        var userId = "nonexistent-user-" + Guid.NewGuid().ToString();
        var request = new { UserId = userId };

        // Act
        var response = await _client.PostAsJsonAsync("/api/orders", request);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_WithEmptyCart_ReturnsBadRequest()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        // Create an empty cart by adding and then removing an item
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Temp Product " + Guid.NewGuid().ToString());

        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = product.Id, Quantity = 1 });
        await _client.DeleteAsync($"/api/carts/{userId}/items/{product.Id}");

        var request = new { UserId = userId };

        // Act
        var response = await _client.PostAsJsonAsync("/api/orders", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_ReducesProductStock()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Stock Test Product " + Guid.NewGuid().ToString(), stock: 10);

        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = product.Id, Quantity = 3 });
        var request = new { UserId = userId };

        // Act
        var response = await _client.PostAsJsonAsync("/api/orders", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        // Verify stock was reduced (refresh from database)
        await _context.Entry(product).ReloadAsync();
        Assert.Equal(7, product.StockQuantity); // 10 - 3 = 7
    }

    #endregion

    #region GET /api/orders/{id} - Get Order

    [Fact]
    public async Task GetOrder_WithExistingOrder_ReturnsOrder()
    {
        // Arrange
        var userId = await SetupCartWithItems();
        var createRequest = new { UserId = userId };
        var createResponse = await _client.PostAsJsonAsync("/api/orders", createRequest);
        var createdOrder = await createResponse.Content.ReadFromJsonAsync<OrderDto>();

        // Act
        var response = await _client.GetAsync($"/api/orders/{createdOrder!.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var order = await response.Content.ReadFromJsonAsync<OrderDto>();
        Assert.NotNull(order);
        Assert.Equal(createdOrder.Id, order.Id);
        Assert.Equal(userId, order.UserId);
    }

    [Fact]
    public async Task GetOrder_WithNonExistingOrder_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync("/api/orders/99999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    #endregion

    #region GET /api/users/{userId}/orders - Get User Orders

    [Fact]
    public async Task GetUserOrders_WithExistingOrders_ReturnsOrderSummaries()
    {
        // Arrange
        var userId = await SetupCartWithItems();
        await _client.PostAsJsonAsync("/api/orders", new { UserId = userId });

        // Create another order
        var categoryId = await SeedCategoryAsync();
        var product2 = await SeedProductAsync(categoryId, "Second Product " + Guid.NewGuid().ToString());
        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = product2.Id, Quantity = 1 });
        await _client.PostAsJsonAsync("/api/orders", new { UserId = userId });

        // Act
        var response = await _client.GetAsync($"/api/users/{userId}/orders");

        // Assert
        response.EnsureSuccessStatusCode();
        var orders = await response.Content.ReadFromJsonAsync<List<OrderSummaryDto>>();
        Assert.NotNull(orders);
        Assert.Equal(2, orders.Count);
    }

    [Fact]
    public async Task GetUserOrders_WithNoOrders_ReturnsEmptyList()
    {
        // Arrange
        var userId = "new-user-" + Guid.NewGuid().ToString();

        // Act
        var response = await _client.GetAsync($"/api/users/{userId}/orders");

        // Assert
        response.EnsureSuccessStatusCode();
        var orders = await response.Content.ReadFromJsonAsync<List<OrderSummaryDto>>();
        Assert.NotNull(orders);
        Assert.Empty(orders);
    }

    #endregion

    #region PUT /api/orders/{id}/status - Update Order Status

    [Fact]
    public async Task UpdateOrderStatus_FromPendingToConfirmed_UpdatesStatus()
    {
        // Arrange
        var userId = await SetupCartWithItems();
        var createResponse = await _client.PostAsJsonAsync("/api/orders", new { UserId = userId });
        var createdOrder = await createResponse.Content.ReadFromJsonAsync<OrderDto>();

        var updateRequest = new { Status = OrderStatus.Confirmed };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/orders/{createdOrder!.Id}/status", updateRequest);

        // Assert
        response.EnsureSuccessStatusCode();
        var order = await response.Content.ReadFromJsonAsync<OrderDto>();
        Assert.NotNull(order);
        Assert.Equal(OrderStatus.Confirmed, order.Status);
        Assert.NotNull(order.ConfirmedAt);
    }

    [Fact]
    public async Task UpdateOrderStatus_InvalidTransition_ReturnsBadRequest()
    {
        // Arrange
        var userId = await SetupCartWithItems();
        var createResponse = await _client.PostAsJsonAsync("/api/orders", new { UserId = userId });
        var createdOrder = await createResponse.Content.ReadFromJsonAsync<OrderDto>();

        // Trying to ship a pending order (should go through confirmed first)
        var updateRequest = new { Status = OrderStatus.Shipped };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/orders/{createdOrder!.Id}/status", updateRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrderStatus_NonExistingOrder_ReturnsNotFound()
    {
        // Arrange
        var updateRequest = new { Status = OrderStatus.Confirmed };

        // Act
        var response = await _client.PutAsJsonAsync("/api/orders/99999/status", updateRequest);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrderStatus_FullWorkflow_CompletesSuccessfully()
    {
        // Arrange
        var userId = await SetupCartWithItems();
        var createResponse = await _client.PostAsJsonAsync("/api/orders", new { UserId = userId });
        var createdOrder = await createResponse.Content.ReadFromJsonAsync<OrderDto>();
        var orderId = createdOrder!.Id;

        // Act - Confirm
        var confirmResponse = await _client.PutAsJsonAsync($"/api/orders/{orderId}/status", new { Status = OrderStatus.Confirmed });
        confirmResponse.EnsureSuccessStatusCode();

        // Act - Ship
        var shipResponse = await _client.PutAsJsonAsync($"/api/orders/{orderId}/status", new { Status = OrderStatus.Shipped });
        shipResponse.EnsureSuccessStatusCode();

        // Act - Deliver
        var deliverResponse = await _client.PutAsJsonAsync($"/api/orders/{orderId}/status", new { Status = OrderStatus.Delivered });
        deliverResponse.EnsureSuccessStatusCode();

        // Assert
        var order = await deliverResponse.Content.ReadFromJsonAsync<OrderDto>();
        Assert.NotNull(order);
        Assert.Equal(OrderStatus.Delivered, order.Status);
        Assert.NotNull(order.ConfirmedAt);
        Assert.NotNull(order.ShippedAt);
        Assert.NotNull(order.DeliveredAt);
        Assert.False(order.CanBeCancelled);
        Assert.False(order.CanBeModified);
    }

    #endregion

    #region POST /api/orders/{id}/cancel - Cancel Order

    [Fact]
    public async Task CancelOrder_PendingOrder_CancelsSuccessfully()
    {
        // Arrange
        var userId = await SetupCartWithItems();
        var createResponse = await _client.PostAsJsonAsync("/api/orders", new { UserId = userId });
        var createdOrder = await createResponse.Content.ReadFromJsonAsync<OrderDto>();

        // Act
        var response = await _client.PostAsync($"/api/orders/{createdOrder!.Id}/cancel", null);

        // Assert
        response.EnsureSuccessStatusCode();
        var order = await response.Content.ReadFromJsonAsync<OrderDto>();
        Assert.NotNull(order);
        Assert.Equal(OrderStatus.Cancelled, order.Status);
        Assert.NotNull(order.CancelledAt);
        Assert.False(order.CanBeCancelled);
    }

    [Fact]
    public async Task CancelOrder_ConfirmedOrder_CancelsSuccessfully()
    {
        // Arrange
        var userId = await SetupCartWithItems();
        var createResponse = await _client.PostAsJsonAsync("/api/orders", new { UserId = userId });
        var createdOrder = await createResponse.Content.ReadFromJsonAsync<OrderDto>();

        // Confirm the order first
        await _client.PutAsJsonAsync($"/api/orders/{createdOrder!.Id}/status", new { Status = OrderStatus.Confirmed });

        // Act
        var response = await _client.PostAsync($"/api/orders/{createdOrder.Id}/cancel", null);

        // Assert
        response.EnsureSuccessStatusCode();
        var order = await response.Content.ReadFromJsonAsync<OrderDto>();
        Assert.NotNull(order);
        Assert.Equal(OrderStatus.Cancelled, order.Status);
    }

    [Fact]
    public async Task CancelOrder_ShippedOrder_ReturnsBadRequest()
    {
        // Arrange
        var userId = await SetupCartWithItems();
        var createResponse = await _client.PostAsJsonAsync("/api/orders", new { UserId = userId });
        var createdOrder = await createResponse.Content.ReadFromJsonAsync<OrderDto>();
        var orderId = createdOrder!.Id;

        // Confirm and ship the order
        await _client.PutAsJsonAsync($"/api/orders/{orderId}/status", new { Status = OrderStatus.Confirmed });
        await _client.PutAsJsonAsync($"/api/orders/{orderId}/status", new { Status = OrderStatus.Shipped });

        // Act
        var response = await _client.PostAsync($"/api/orders/{orderId}/cancel", null);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CancelOrder_NonExistingOrder_ReturnsNotFound()
    {
        // Act
        var response = await _client.PostAsync("/api/orders/99999/cancel", null);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CancelOrder_RestoresProductStock()
    {
        // Arrange
        var userId = "user-" + Guid.NewGuid().ToString();
        var categoryId = await SeedCategoryAsync();
        var product = await SeedProductAsync(categoryId, "Cancel Stock Test " + Guid.NewGuid().ToString(), stock: 10);

        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = product.Id, Quantity = 3 });
        var createResponse = await _client.PostAsJsonAsync("/api/orders", new { UserId = userId });
        var createdOrder = await createResponse.Content.ReadFromJsonAsync<OrderDto>();

        // Verify stock was reduced
        await _context.Entry(product).ReloadAsync();
        Assert.Equal(7, product.StockQuantity);

        // Act
        await _client.PostAsync($"/api/orders/{createdOrder!.Id}/cancel", null);

        // Assert - Stock should be restored
        await _context.Entry(product).ReloadAsync();
        Assert.Equal(10, product.StockQuantity);
    }

    #endregion

    #region Order Total Calculation

    [Fact]
    public async Task CreateOrder_CalculatesTotalCorrectly()
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
        var response = await _client.PostAsJsonAsync("/api/orders", new { UserId = userId });

        // Assert
        response.EnsureSuccessStatusCode();
        var order = await response.Content.ReadFromJsonAsync<OrderDto>();
        Assert.NotNull(order);
        Assert.Equal(35.00m, order.TotalAmount);
        Assert.Equal(5, order.ItemCount);
    }

    #endregion
}
