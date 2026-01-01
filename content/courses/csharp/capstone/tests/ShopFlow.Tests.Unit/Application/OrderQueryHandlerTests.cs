using NSubstitute;
using ShopFlow.Application.Orders.Handlers;
using ShopFlow.Application.Orders.Queries;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Enums;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Application;

public class OrderQueryHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly OrderQueryHandler _handler;

    public OrderQueryHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _handler = new OrderQueryHandler(_orderRepository);
    }

    private Order CreateOrder(string userId = "user-123")
    {
        var cart = Cart.Create(userId);
        cart.AddItem(1, 2, Money.Create(10.00m, "USD"));
        cart.AddItem(2, 1, Money.Create(25.00m, "USD"));

        var productNames = new Dictionary<int, string>
        {
            { 1, "Product One" },
            { 2, "Product Two" }
        };

        return Order.CreateFromCart(cart, productNames, "123 Main St");
    }

    #region GetOrderQuery Tests

    [Fact]
    public async Task Handle_GetOrderQuery_WithExistingOrder_ReturnsOrderDto()
    {
        // Arrange
        var order = CreateOrder();
        _orderRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(order);

        var query = new GetOrderQuery(1);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("user-123", result.UserId);
        Assert.Equal(OrderStatus.Pending, result.Status);
        Assert.Equal("Pending", result.StatusName);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(45.00m, result.TotalAmount); // (2*10) + (1*25)
        Assert.Equal("123 Main St", result.ShippingAddress);
        Assert.True(result.CanBeCancelled);
        Assert.True(result.CanBeModified);
    }

    [Fact]
    public async Task Handle_GetOrderQuery_WithNonExistentOrder_ThrowsEntityNotFoundException()
    {
        // Arrange
        _orderRepository.GetByIdAsync(999, Arg.Any<CancellationToken>())
            .Returns((Order?)null);

        var query = new GetOrderQuery(999);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.HandleAsync(query));
    }

    [Fact]
    public async Task Handle_GetOrderQuery_MapsItemsCorrectly()
    {
        // Arrange
        var order = CreateOrder();
        _orderRepository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(order);

        var query = new GetOrderQuery(1);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        var firstItem = result.Items.First();
        Assert.Equal("Product One", firstItem.ProductName);
        Assert.Equal(2, firstItem.Quantity);
        Assert.Equal(10.00m, firstItem.UnitPrice);
        Assert.Equal(20.00m, firstItem.Subtotal);
        Assert.Equal("USD", firstItem.Currency);
    }

    #endregion

    #region GetUserOrdersQuery Tests

    [Fact]
    public async Task Handle_GetUserOrdersQuery_WithExistingOrders_ReturnsOrderSummaries()
    {
        // Arrange
        var order1 = CreateOrder("user-123");
        var order2 = CreateOrder("user-123");

        _orderRepository.GetByUserIdAsync("user-123", Arg.Any<CancellationToken>())
            .Returns(new List<Order> { order1, order2 });

        var query = new GetUserOrdersQuery("user-123");

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task Handle_GetUserOrdersQuery_WithNoOrders_ReturnsEmptyList()
    {
        // Arrange
        _orderRepository.GetByUserIdAsync("user-456", Arg.Any<CancellationToken>())
            .Returns(new List<Order>());

        var query = new GetUserOrdersQuery("user-456");

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task Handle_GetUserOrdersQuery_ReturnsSummaryWithCorrectData()
    {
        // Arrange
        var order = CreateOrder("user-123");
        _orderRepository.GetByUserIdAsync("user-123", Arg.Any<CancellationToken>())
            .Returns(new List<Order> { order });

        var query = new GetUserOrdersQuery("user-123");

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        var summary = result.First();
        Assert.Equal(OrderStatus.Pending, summary.Status);
        Assert.Equal("Pending", summary.StatusName);
        Assert.Equal(45.00m, summary.TotalAmount);
        Assert.Equal("USD", summary.Currency);
        Assert.Equal(3, summary.ItemCount); // 2 + 1
    }

    #endregion
}
