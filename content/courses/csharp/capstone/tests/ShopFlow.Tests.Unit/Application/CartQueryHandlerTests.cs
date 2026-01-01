using NSubstitute;
using ShopFlow.Application.Carts.Handlers;
using ShopFlow.Application.Carts.Queries;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Application;

public class CartQueryHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly CartQueryHandler _handler;

    public CartQueryHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new CartQueryHandler(_cartRepository, _productRepository);
    }

    [Fact]
    public async Task HandleAsync_GetCart_WithExistingCart_ReturnsCartDto()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(29.99m, "USD"));

        var product = Product.Create("Test Product", "Description", Money.Create(29.99m, "USD"), 1, 100);

        var query = new GetCartQuery("user-123");

        _cartRepository.GetByUserIdAsync(query.UserId, Arg.Any<CancellationToken>())
            .Returns(cart);
        _productRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(product);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("user-123", result.UserId);
        Assert.Single(result.Items);
        Assert.Equal("Test Product", result.Items.First().ProductName);
    }

    [Fact]
    public async Task HandleAsync_GetCart_WithNonExistentCart_ReturnsEmptyCart()
    {
        // Arrange
        var query = new GetCartQuery("user-123");

        _cartRepository.GetByUserIdAsync(query.UserId, Arg.Any<CancellationToken>())
            .Returns((Cart?)null);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("user-123", result.UserId);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalAmount);
    }

    [Fact]
    public async Task HandleAsync_GetCart_CalculatesTotalCorrectly()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(10.00m, "USD")); // 20.00
        cart.AddItem(2, 3, Money.Create(5.00m, "USD"));  // 15.00

        var product1 = Product.Create("Product 1", "Desc", Money.Create(10.00m, "USD"), 1, 100);
        var product2 = Product.Create("Product 2", "Desc", Money.Create(5.00m, "USD"), 1, 100);

        var query = new GetCartQuery("user-123");

        _cartRepository.GetByUserIdAsync(query.UserId, Arg.Any<CancellationToken>())
            .Returns(cart);
        _productRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(product1);
        _productRepository.GetByIdAsync(2, Arg.Any<CancellationToken>())
            .Returns(product2);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.Equal(35.00m, result.TotalAmount);
        Assert.Equal(5, result.ItemCount);
    }
}
