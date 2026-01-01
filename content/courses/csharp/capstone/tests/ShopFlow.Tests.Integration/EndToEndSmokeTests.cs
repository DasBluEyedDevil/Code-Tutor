using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopFlow.Application.Auth.DTOs;
using ShopFlow.Application.Carts.DTOs;
using ShopFlow.Application.Orders.DTOs;
using ShopFlow.Application.Products.DTOs;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Enums;
using ShopFlow.Domain.ValueObjects;
using ShopFlow.Infrastructure.Persistence;

namespace ShopFlow.Tests.Integration;

/// <summary>
/// End-to-end smoke tests that verify the complete user journey through ShopFlow.
/// These tests simulate real-world usage patterns and verify all components work together.
/// </summary>
public class EndToEndSmokeTestFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = "SmokeTestDb_" + Guid.NewGuid().ToString();

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

public class EndToEndSmokeTests : IClassFixture<EndToEndSmokeTestFactory>, IDisposable
{
    private readonly EndToEndSmokeTestFactory _factory;
    private readonly HttpClient _client;
    private readonly IServiceScope _scope;
    private readonly AppDbContext _context;

    public EndToEndSmokeTests(EndToEndSmokeTestFactory factory)
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

    /// <summary>
    /// Complete e-commerce journey: Register -> Login -> Browse Products -> Add to Cart -> Checkout -> Order Delivery
    /// This test verifies all major system components work together.
    /// </summary>
    [Fact]
    public async Task FullShoppingJourney_RegisterToOrderDelivery_CompletesSuccessfully()
    {
        // ===== STEP 1: Register a new user =====
        var uniqueEmail = $"shopper_{Guid.NewGuid()}@example.com";
        var registerRequest = new
        {
            Email = uniqueEmail,
            Password = "SecurePass123!",
            FirstName = "Jane",
            LastName = "Shopper"
        };

        var registerResponse = await _client.PostAsJsonAsync("/api/auth/register", registerRequest);
        Assert.Equal(HttpStatusCode.Created, registerResponse.StatusCode);

        var authResult = await registerResponse.Content.ReadFromJsonAsync<AuthResultDto>();
        Assert.NotNull(authResult);
        Assert.NotEmpty(authResult.AccessToken);
        Assert.NotEmpty(authResult.RefreshToken);
        Assert.Equal(uniqueEmail, authResult.User.Email);
        Assert.Equal("Jane", authResult.User.FirstName);

        // ===== STEP 2: Set authorization header =====
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

        // ===== STEP 3: Seed products (simulating admin created products) =====
        var category = Category.Create("Electronics", "Electronic devices and gadgets");
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var laptop = Product.Create("Gaming Laptop", "High-performance gaming laptop", Money.Create(1299.99m, "USD"), category.Id, 10);
        var mouse = Product.Create("Wireless Mouse", "Ergonomic wireless mouse", Money.Create(49.99m, "USD"), category.Id, 50);
        var keyboard = Product.Create("Mechanical Keyboard", "RGB mechanical keyboard", Money.Create(129.99m, "USD"), category.Id, 25);
        _context.Products.AddRange(laptop, mouse, keyboard);
        await _context.SaveChangesAsync();

        // ===== STEP 4: Browse products (public endpoint) =====
        var productsResponse = await _client.GetAsync("/api/products");
        Assert.Equal(HttpStatusCode.OK, productsResponse.StatusCode);
        var products = await productsResponse.Content.ReadFromJsonAsync<PagedResultDto<ProductSummaryDto>>();
        Assert.NotNull(products);
        Assert.Equal(3, products.Items.Count);

        // ===== STEP 5: View product details =====
        var laptopDetailsResponse = await _client.GetAsync($"/api/products/{laptop.Id}");
        Assert.Equal(HttpStatusCode.OK, laptopDetailsResponse.StatusCode);
        var laptopDetails = await laptopDetailsResponse.Content.ReadFromJsonAsync<ProductDto>();
        Assert.NotNull(laptopDetails);
        Assert.Equal("Gaming Laptop", laptopDetails.Name);
        Assert.Equal(1299.99m, laptopDetails.Price);

        // ===== STEP 6: Add items to cart =====
        var userId = authResult.User.Id;

        // Add laptop (1x)
        var addLaptopResponse = await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = laptop.Id, Quantity = 1 });
        Assert.Equal(HttpStatusCode.OK, addLaptopResponse.StatusCode);

        // Add mouse (2x)
        var addMouseResponse = await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = mouse.Id, Quantity = 2 });
        Assert.Equal(HttpStatusCode.OK, addMouseResponse.StatusCode);

        // Add keyboard (1x)
        var addKeyboardResponse = await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = keyboard.Id, Quantity = 1 });
        Assert.Equal(HttpStatusCode.OK, addKeyboardResponse.StatusCode);

        // ===== STEP 7: Review cart =====
        var cartResponse = await _client.GetAsync($"/api/carts/{userId}");
        Assert.Equal(HttpStatusCode.OK, cartResponse.StatusCode);
        var cart = await cartResponse.Content.ReadFromJsonAsync<CartDto>();
        Assert.NotNull(cart);
        Assert.Equal(3, cart.Items.Count);
        Assert.Equal(4, cart.ItemCount); // 1 + 2 + 1 = 4 total items

        // Expected total: 1299.99 + (49.99 * 2) + 129.99 = 1529.96
        Assert.Equal(1529.96m, cart.TotalAmount);

        // ===== STEP 8: Update cart (change mouse quantity to 1) =====
        var updateCartResponse = await _client.PutAsJsonAsync($"/api/carts/{userId}/items/{mouse.Id}", new { Quantity = 1 });
        Assert.Equal(HttpStatusCode.OK, updateCartResponse.StatusCode);

        // Verify updated cart
        var updatedCartResponse = await _client.GetAsync($"/api/carts/{userId}");
        var updatedCart = await updatedCartResponse.Content.ReadFromJsonAsync<CartDto>();
        Assert.NotNull(updatedCart);
        Assert.Equal(3, updatedCart.ItemCount); // Now 1 + 1 + 1 = 3
        Assert.Equal(1479.97m, updatedCart.TotalAmount); // 1299.99 + 49.99 + 129.99 = 1479.97

        // ===== STEP 9: Create order (checkout) =====
        var createOrderRequest = new { UserId = userId, ShippingAddress = "123 Main Street, Apt 4B, New York, NY 10001" };
        var createOrderResponse = await _client.PostAsJsonAsync("/api/orders", createOrderRequest);
        Assert.Equal(HttpStatusCode.Created, createOrderResponse.StatusCode);

        var order = await createOrderResponse.Content.ReadFromJsonAsync<OrderDto>();
        Assert.NotNull(order);
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Equal("123 Main Street, Apt 4B, New York, NY 10001", order.ShippingAddress);
        Assert.Equal(3, order.Items.Count);
        Assert.Equal(1479.97m, order.TotalAmount);
        Assert.True(order.CanBeCancelled);
        Assert.True(order.CanBeModified);

        // ===== STEP 10: Verify cart is cleared after order =====
        var clearedCartResponse = await _client.GetAsync($"/api/carts/{userId}");
        var clearedCart = await clearedCartResponse.Content.ReadFromJsonAsync<CartDto>();
        Assert.NotNull(clearedCart);
        Assert.Empty(clearedCart.Items);

        // ===== STEP 11: Verify stock was reduced =====
        await _context.Entry(laptop).ReloadAsync();
        await _context.Entry(mouse).ReloadAsync();
        await _context.Entry(keyboard).ReloadAsync();
        Assert.Equal(9, laptop.StockQuantity);  // 10 - 1
        Assert.Equal(49, mouse.StockQuantity);  // 50 - 1
        Assert.Equal(24, keyboard.StockQuantity); // 25 - 1

        // ===== STEP 12: Order workflow - Confirm =====
        var confirmResponse = await _client.PutAsJsonAsync($"/api/orders/{order.Id}/status", new { Status = OrderStatus.Confirmed });
        Assert.Equal(HttpStatusCode.OK, confirmResponse.StatusCode);
        var confirmedOrder = await confirmResponse.Content.ReadFromJsonAsync<OrderDto>();
        Assert.NotNull(confirmedOrder);
        Assert.Equal(OrderStatus.Confirmed, confirmedOrder.Status);
        Assert.NotNull(confirmedOrder.ConfirmedAt);

        // ===== STEP 13: Order workflow - Ship =====
        var shipResponse = await _client.PutAsJsonAsync($"/api/orders/{order.Id}/status", new { Status = OrderStatus.Shipped });
        Assert.Equal(HttpStatusCode.OK, shipResponse.StatusCode);
        var shippedOrder = await shipResponse.Content.ReadFromJsonAsync<OrderDto>();
        Assert.NotNull(shippedOrder);
        Assert.Equal(OrderStatus.Shipped, shippedOrder.Status);
        Assert.NotNull(shippedOrder.ShippedAt);
        Assert.False(shippedOrder.CanBeCancelled); // Can't cancel shipped orders

        // ===== STEP 14: Order workflow - Deliver =====
        var deliverResponse = await _client.PutAsJsonAsync($"/api/orders/{order.Id}/status", new { Status = OrderStatus.Delivered });
        Assert.Equal(HttpStatusCode.OK, deliverResponse.StatusCode);
        var deliveredOrder = await deliverResponse.Content.ReadFromJsonAsync<OrderDto>();
        Assert.NotNull(deliveredOrder);
        Assert.Equal(OrderStatus.Delivered, deliveredOrder.Status);
        Assert.NotNull(deliveredOrder.DeliveredAt);
        Assert.False(deliveredOrder.CanBeModified);

        // ===== STEP 15: View order history =====
        var orderHistoryResponse = await _client.GetAsync($"/api/users/{userId}/orders");
        Assert.Equal(HttpStatusCode.OK, orderHistoryResponse.StatusCode);
        var orderHistory = await orderHistoryResponse.Content.ReadFromJsonAsync<List<OrderSummaryDto>>();
        Assert.NotNull(orderHistory);
        Assert.Single(orderHistory);
        Assert.Equal(OrderStatus.Delivered, orderHistory.First().Status);

        // ===== STEP 16: Refresh token =====
        var refreshRequest = new { RefreshToken = authResult.RefreshToken };
        var refreshResponse = await _client.PostAsJsonAsync("/api/auth/refresh", refreshRequest);
        Assert.Equal(HttpStatusCode.OK, refreshResponse.StatusCode);
        var newTokens = await refreshResponse.Content.ReadFromJsonAsync<TokenRefreshResultDto>();
        Assert.NotNull(newTokens);
        Assert.NotEmpty(newTokens.AccessToken);
        Assert.NotEqual(authResult.AccessToken, newTokens.AccessToken);

        // ===== STEP 17: Logout =====
        var logoutRequest = new { RefreshToken = newTokens.RefreshToken };
        var logoutResponse = await _client.PostAsJsonAsync("/api/auth/logout", logoutRequest);
        Assert.Equal(HttpStatusCode.NoContent, logoutResponse.StatusCode);
    }

    /// <summary>
    /// Verifies that order cancellation restores product stock and properly updates order status.
    /// </summary>
    [Fact]
    public async Task OrderCancellation_RestoresStockAndUpdatesStatus()
    {
        // Setup: Register and authenticate
        var uniqueEmail = $"canceller_{Guid.NewGuid()}@example.com";
        var registerResponse = await _client.PostAsJsonAsync("/api/auth/register", new
        {
            Email = uniqueEmail,
            Password = "SecurePass123!",
            FirstName = "Cancel",
            LastName = "Tester"
        });
        var authResult = await registerResponse.Content.ReadFromJsonAsync<AuthResultDto>();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult!.AccessToken);

        // Setup: Create product with limited stock
        var category = Category.Create("Limited Edition", "Rare items");
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var rareItem = Product.Create("Collector's Edition", "Limited availability", Money.Create(199.99m, "USD"), category.Id, 5);
        _context.Products.Add(rareItem);
        await _context.SaveChangesAsync();

        var userId = authResult.User.Id;

        // Add to cart and create order
        await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new { ProductId = rareItem.Id, Quantity = 3 });
        var createOrderResponse = await _client.PostAsJsonAsync("/api/orders", new { UserId = userId });
        var order = await createOrderResponse.Content.ReadFromJsonAsync<OrderDto>();

        // Verify stock was reduced
        await _context.Entry(rareItem).ReloadAsync();
        Assert.Equal(2, rareItem.StockQuantity); // 5 - 3 = 2

        // Cancel the order
        var cancelResponse = await _client.PostAsync($"/api/orders/{order!.Id}/cancel", null);
        Assert.Equal(HttpStatusCode.OK, cancelResponse.StatusCode);

        var cancelledOrder = await cancelResponse.Content.ReadFromJsonAsync<OrderDto>();
        Assert.NotNull(cancelledOrder);
        Assert.Equal(OrderStatus.Cancelled, cancelledOrder.Status);
        Assert.NotNull(cancelledOrder.CancelledAt);

        // Verify stock was restored
        await _context.Entry(rareItem).ReloadAsync();
        Assert.Equal(5, rareItem.StockQuantity); // Back to original
    }

    /// <summary>
    /// Verifies that unauthenticated requests to protected endpoints are rejected.
    /// </summary>
    [Fact]
    public async Task ProtectedEndpoints_WithoutAuth_ReturnUnauthorized()
    {
        // Create a fresh client without auth header
        using var unauthClient = _factory.CreateClient();

        // Cart endpoints require auth
        var cartResponse = await unauthClient.GetAsync("/api/carts/test-user");
        Assert.Equal(HttpStatusCode.Unauthorized, cartResponse.StatusCode);

        // Order endpoints require auth
        var ordersResponse = await unauthClient.GetAsync("/api/users/test-user/orders");
        Assert.Equal(HttpStatusCode.Unauthorized, ordersResponse.StatusCode);

        // But products are public
        var productsResponse = await unauthClient.GetAsync("/api/products");
        Assert.Equal(HttpStatusCode.OK, productsResponse.StatusCode);
    }

    /// <summary>
    /// Verifies duplicate email registration is rejected.
    /// </summary>
    [Fact]
    public async Task DuplicateRegistration_IsRejected()
    {
        var uniqueEmail = $"duplicate_{Guid.NewGuid()}@example.com";
        var registerRequest = new
        {
            Email = uniqueEmail,
            Password = "SecurePass123!",
            FirstName = "First",
            LastName = "User"
        };

        // First registration should succeed
        var firstResponse = await _client.PostAsJsonAsync("/api/auth/register", registerRequest);
        Assert.Equal(HttpStatusCode.Created, firstResponse.StatusCode);

        // Second registration with same email should fail
        var secondResponse = await _client.PostAsJsonAsync("/api/auth/register", registerRequest);
        Assert.Equal(HttpStatusCode.BadRequest, secondResponse.StatusCode);
    }

    /// <summary>
    /// Verifies insufficient stock prevents order creation.
    /// </summary>
    [Fact]
    public async Task InsufficientStock_PreventsOrderCreation()
    {
        // Setup: Register and authenticate
        var uniqueEmail = $"stocktest_{Guid.NewGuid()}@example.com";
        var registerResponse = await _client.PostAsJsonAsync("/api/auth/register", new
        {
            Email = uniqueEmail,
            Password = "SecurePass123!",
            FirstName = "Stock",
            LastName = "Tester"
        });
        var authResult = await registerResponse.Content.ReadFromJsonAsync<AuthResultDto>();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult!.AccessToken);

        // Setup: Create product with very limited stock
        var category = Category.Create("Low Stock", "Almost sold out");
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var lowStockItem = Product.Create("Last One", "Only 2 left!", Money.Create(99.99m, "USD"), category.Id, 2);
        _context.Products.Add(lowStockItem);
        await _context.SaveChangesAsync();

        var userId = authResult.User.Id;

        // Try to add more than available stock
        var addResponse = await _client.PostAsJsonAsync($"/api/carts/{userId}/items",
            new { ProductId = lowStockItem.Id, Quantity = 5 });

        Assert.Equal(HttpStatusCode.BadRequest, addResponse.StatusCode);
    }
}
