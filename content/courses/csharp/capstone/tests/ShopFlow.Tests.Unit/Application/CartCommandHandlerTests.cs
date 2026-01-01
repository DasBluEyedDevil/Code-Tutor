using NSubstitute;
using ShopFlow.Application.Carts.Commands;
using ShopFlow.Application.Carts.Handlers;
using ShopFlow.Application.Common.Interfaces;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Application;

public class CartCommandHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CartCommandHandler _handler;

    public CartCommandHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new CartCommandHandler(_cartRepository, _productRepository, _unitOfWork);
    }

    #region AddToCart Tests

    [Fact]
    public async Task HandleAsync_AddToCart_WithNewCart_CreatesCartAndAddsItem()
    {
        // Arrange
        var command = new AddToCartCommand("user-123", 1, 2);
        var product = Product.Create("Test Product", "Description", Money.Create(29.99m, "USD"), 1, 100);

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns((Cart?)null);
        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(product);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("user-123", result.UserId);
        Assert.Single(result.Items);
        Assert.Equal(1, result.Items.First().ProductId);
        Assert.Equal(2, result.Items.First().Quantity);

        await _cartRepository.Received(1).AddAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_AddToCart_WithExistingCart_AddsItemToCart()
    {
        // Arrange
        var command = new AddToCartCommand("user-123", 1, 2);
        var cart = Cart.Create("user-123");
        var product = Product.Create("Test Product", "Description", Money.Create(29.99m, "USD"), 1, 100);

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(cart);
        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(product);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Items);

        _cartRepository.Received(1).Update(cart);
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_AddToCart_WithExistingProduct_UpdatesQuantity()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        var product = Product.Create("Test Product", "Description", Money.Create(29.99m, "USD"), 1, 100);
        cart.AddItem(1, 2, product.Price);

        var command = new AddToCartCommand("user-123", 1, 3);

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(cart);
        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(product);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.Single(result.Items);
        Assert.Equal(5, result.Items.First().Quantity);
    }

    [Fact]
    public async Task HandleAsync_AddToCart_WithNonExistentProduct_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new AddToCartCommand("user-123", 999, 2);

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns((Cart?)null);
        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns((Product?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("Product", exception.Message);
        Assert.Contains("999", exception.Message);

        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_AddToCart_WithInsufficientStock_ThrowsDomainException()
    {
        // Arrange
        var command = new AddToCartCommand("user-123", 1, 150);
        var product = Product.Create("Test Product", "Description", Money.Create(29.99m, "USD"), 1, 100);

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns((Cart?)null);
        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(product);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DomainException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("stock", exception.Message.ToLower());

        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region RemoveFromCart Tests

    [Fact]
    public async Task HandleAsync_RemoveFromCart_WithExistingItem_RemovesItem()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(29.99m, "USD"));
        cart.AddItem(2, 1, Money.Create(19.99m, "USD"));

        var command = new RemoveFromCartCommand("user-123", 1);

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(cart);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.Single(result.Items);
        Assert.Equal(2, result.Items.First().ProductId);

        _cartRepository.Received(1).Update(cart);
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_RemoveFromCart_WithNonExistentCart_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new RemoveFromCartCommand("user-123", 1);

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns((Cart?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("Cart", exception.Message);
    }

    [Fact]
    public async Task HandleAsync_RemoveFromCart_WithNonExistentItem_ThrowsDomainException()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(29.99m, "USD"));

        var command = new RemoveFromCartCommand("user-123", 999);

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(cart);

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(
            () => _handler.HandleAsync(command));
    }

    #endregion

    #region UpdateCartItemQuantity Tests

    [Fact]
    public async Task HandleAsync_UpdateCartItemQuantity_WithValidQuantity_UpdatesQuantity()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(29.99m, "USD"));

        var command = new UpdateCartItemQuantityCommand("user-123", 1, 5);

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(cart);
        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(Product.Create("Test", "Desc", Money.Create(29.99m, "USD"), 1, 100));
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.Single(result.Items);
        Assert.Equal(5, result.Items.First().Quantity);
    }

    [Fact]
    public async Task HandleAsync_UpdateCartItemQuantity_WithZeroQuantity_RemovesItem()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(29.99m, "USD"));

        var command = new UpdateCartItemQuantityCommand("user-123", 1, 0);

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(cart);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task HandleAsync_UpdateCartItemQuantity_WithInsufficientStock_ThrowsDomainException()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(29.99m, "USD"));

        var command = new UpdateCartItemQuantityCommand("user-123", 1, 150);

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(cart);
        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(Product.Create("Test", "Desc", Money.Create(29.99m, "USD"), 1, 100));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DomainException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("stock", exception.Message.ToLower());
    }

    #endregion

    #region ClearCart Tests

    [Fact]
    public async Task HandleAsync_ClearCart_RemovesAllItems()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(29.99m, "USD"));
        cart.AddItem(2, 1, Money.Create(19.99m, "USD"));

        var command = new ClearCartCommand("user-123");

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(cart);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        Assert.Empty(cart.Items);
        _cartRepository.Received(1).Update(cart);
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_ClearCart_WithNonExistentCart_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new ClearCartCommand("user-123");

        _cartRepository.GetByUserIdAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns((Cart?)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _handler.HandleAsync(command));
    }

    #endregion
}
