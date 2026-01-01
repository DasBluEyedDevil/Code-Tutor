using NSubstitute;
using ShopFlow.Application.Common.Interfaces;
using ShopFlow.Application.Orders.Commands;
using ShopFlow.Application.Orders.Handlers;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Enums;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Application;

public class OrderCommandHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly OrderCommandHandler _handler;

    public OrderCommandHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _cartRepository = Substitute.For<ICartRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();

        _handler = new OrderCommandHandler(
            _orderRepository,
            _cartRepository,
            _productRepository,
            _unitOfWork
        );
    }

    private Cart CreateCartWithItems(string userId = "user-123")
    {
        var cart = Cart.Create(userId);
        cart.AddItem(1, 2, Money.Create(10.00m, "USD"));
        cart.AddItem(2, 1, Money.Create(25.00m, "USD"));
        return cart;
    }

    private Product CreateProduct(int id, string name, int stock = 100)
    {
        return Product.Create(name, "Description", Money.Create(10.00m, "USD"), 1, stock);
    }

    #region CreateOrderCommand Tests

    [Fact]
    public async Task Handle_CreateOrderCommand_WithValidCart_CreatesOrder()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var product1 = CreateProduct(1, "Product One");
        var product2 = CreateProduct(2, "Product Two");

        _cartRepository.GetByUserIdAsync("user-123", Arg.Any<CancellationToken>())
            .Returns(cart);
        _productRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(product1);
        _productRepository.GetByIdAsync(2, Arg.Any<CancellationToken>())
            .Returns(product2);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        var command = new CreateOrderCommand("user-123", "123 Main St");

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("user-123", result.UserId);
        Assert.Equal(OrderStatus.Pending, result.Status);
        Assert.Equal("123 Main St", result.ShippingAddress);
        Assert.Equal(2, result.Items.Count);

        await _orderRepository.Received(1).AddAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
        _cartRepository.Received(1).Update(Arg.Any<Cart>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_CreateOrderCommand_WithNoCart_ThrowsEntityNotFoundException()
    {
        // Arrange
        _cartRepository.GetByUserIdAsync("user-123", Arg.Any<CancellationToken>())
            .Returns((Cart?)null);

        var command = new CreateOrderCommand("user-123");

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.HandleAsync(command));
    }

    [Fact]
    public async Task Handle_CreateOrderCommand_WithEmptyCart_ThrowsDomainException()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        _cartRepository.GetByUserIdAsync("user-123", Arg.Any<CancellationToken>())
            .Returns(cart);

        var command = new CreateOrderCommand("user-123");

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => _handler.HandleAsync(command));
    }

    [Fact]
    public async Task Handle_CreateOrderCommand_WithInsufficientStock_ThrowsDomainException()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var product1 = CreateProduct(1, "Product One", stock: 1); // Only 1 in stock, cart has 2

        _cartRepository.GetByUserIdAsync("user-123", Arg.Any<CancellationToken>())
            .Returns(cart);
        _productRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(product1);

        var command = new CreateOrderCommand("user-123");

        // Act & Assert
        var ex = await Assert.ThrowsAsync<DomainException>(() => _handler.HandleAsync(command));
        Assert.Contains("Insufficient stock", ex.Message);
    }

    [Fact]
    public async Task Handle_CreateOrderCommand_WithMissingProduct_ThrowsEntityNotFoundException()
    {
        // Arrange
        var cart = CreateCartWithItems();

        _cartRepository.GetByUserIdAsync("user-123", Arg.Any<CancellationToken>())
            .Returns(cart);
        _productRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns((Product?)null);

        var command = new CreateOrderCommand("user-123");

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.HandleAsync(command));
    }

    [Fact]
    public async Task Handle_CreateOrderCommand_ClearsCartAfterOrderCreation()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var product1 = CreateProduct(1, "Product One");
        var product2 = CreateProduct(2, "Product Two");

        _cartRepository.GetByUserIdAsync("user-123", Arg.Any<CancellationToken>())
            .Returns(cart);
        _productRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(product1);
        _productRepository.GetByIdAsync(2, Arg.Any<CancellationToken>())
            .Returns(product2);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        var command = new CreateOrderCommand("user-123");

        // Act
        await _handler.HandleAsync(command);

        // Assert - Cart should be cleared (Update called with cart that has no items)
        _cartRepository.Received(1).Update(Arg.Is<Cart>(c => c.Items.Count == 0));
    }

    #endregion

    #region UpdateOrderStatusCommand Tests

    [Fact]
    public async Task Handle_UpdateOrderStatusCommand_WithValidTransition_UpdatesStatus()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var productNames = new Dictionary<int, string> { { 1, "Product One" }, { 2, "Product Two" } };
        var order = Order.CreateFromCart(cart, productNames);

        _orderRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(order);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        var command = new UpdateOrderStatusCommand(1, OrderStatus.Confirmed);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.Equal(OrderStatus.Confirmed, result.Status);
        _orderRepository.Received(1).Update(Arg.Any<Order>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_UpdateOrderStatusCommand_WithInvalidTransition_ThrowsDomainException()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var productNames = new Dictionary<int, string> { { 1, "Product One" }, { 2, "Product Two" } };
        var order = Order.CreateFromCart(cart, productNames);
        // Order is Pending, trying to Ship directly

        _orderRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(order);

        var command = new UpdateOrderStatusCommand(1, OrderStatus.Shipped);

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => _handler.HandleAsync(command));
    }

    [Fact]
    public async Task Handle_UpdateOrderStatusCommand_WithNonExistentOrder_ThrowsEntityNotFoundException()
    {
        // Arrange
        _orderRepository.GetByIdAsync(999, Arg.Any<CancellationToken>())
            .Returns((Order?)null);

        var command = new UpdateOrderStatusCommand(999, OrderStatus.Confirmed);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.HandleAsync(command));
    }

    #endregion

    #region CancelOrderCommand Tests

    [Fact]
    public async Task Handle_CancelOrderCommand_WithPendingOrder_CancelsOrder()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var productNames = new Dictionary<int, string> { { 1, "Product One" }, { 2, "Product Two" } };
        var order = Order.CreateFromCart(cart, productNames);
        var product1 = CreateProduct(1, "Product One", stock: 50);
        var product2 = CreateProduct(2, "Product Two", stock: 50);

        _orderRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(order);
        _productRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(product1);
        _productRepository.GetByIdAsync(2, Arg.Any<CancellationToken>())
            .Returns(product2);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        var command = new CancelOrderCommand(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.Equal(OrderStatus.Cancelled, result.Status);
        _orderRepository.Received(1).Update(Arg.Any<Order>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_CancelOrderCommand_WithShippedOrder_ThrowsDomainException()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var productNames = new Dictionary<int, string> { { 1, "Product One" }, { 2, "Product Two" } };
        var order = Order.CreateFromCart(cart, productNames);
        order.Confirm();
        order.Ship();

        _orderRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(order);

        var command = new CancelOrderCommand(1);

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => _handler.HandleAsync(command));
    }

    [Fact]
    public async Task Handle_CancelOrderCommand_WithNonExistentOrder_ThrowsEntityNotFoundException()
    {
        // Arrange
        _orderRepository.GetByIdAsync(999, Arg.Any<CancellationToken>())
            .Returns((Order?)null);

        var command = new CancelOrderCommand(999);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.HandleAsync(command));
    }

    #endregion
}
