using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Enums;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Domain;

public class OrderTests
{
    private Cart CreateCartWithItems(string userId = "user-123")
    {
        var cart = Cart.Create(userId);
        cart.AddItem(1, 2, Money.Create(10.00m, "USD"));
        cart.AddItem(2, 1, Money.Create(25.00m, "USD"));
        return cart;
    }

    private Dictionary<int, string> CreateProductNames()
    {
        return new Dictionary<int, string>
        {
            { 1, "Product One" },
            { 2, "Product Two" }
        };
    }

    #region CreateFromCart Tests

    [Fact]
    public void CreateFromCart_WithValidCart_CreatesOrder()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var productNames = CreateProductNames();

        // Act
        var order = Order.CreateFromCart(cart, productNames);

        // Assert
        Assert.Equal("user-123", order.UserId);
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Equal(2, order.Items.Count);
        Assert.NotEqual(default, order.CreatedAt);
    }

    [Fact]
    public void CreateFromCart_WithShippingAddress_SetsAddress()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var productNames = CreateProductNames();
        var address = "123 Main St, City, State 12345";

        // Act
        var order = Order.CreateFromCart(cart, productNames, address);

        // Assert
        Assert.Equal(address, order.ShippingAddress);
    }

    [Fact]
    public void CreateFromCart_WithEmptyCart_ThrowsDomainException()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        var productNames = new Dictionary<int, string>();

        // Act & Assert
        var ex = Assert.Throws<DomainException>(() => Order.CreateFromCart(cart, productNames));
        Assert.Contains("empty cart", ex.Message);
    }

    [Fact]
    public void CreateFromCart_WithMissingProductName_ThrowsDomainException()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var productNames = new Dictionary<int, string> { { 1, "Product One" } }; // Missing product 2

        // Act & Assert
        var ex = Assert.Throws<DomainException>(() => Order.CreateFromCart(cart, productNames));
        Assert.Contains("Product name not found", ex.Message);
    }

    [Fact]
    public void CreateFromCart_CopiesItemsAsSnapshots()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var productNames = CreateProductNames();

        // Act
        var order = Order.CreateFromCart(cart, productNames);

        // Assert
        var firstItem = order.Items.First();
        Assert.Equal(1, firstItem.ProductId);
        Assert.Equal("Product One", firstItem.ProductName);
        Assert.Equal(2, firstItem.Quantity);
        Assert.Equal(10.00m, firstItem.UnitPrice.Amount);
    }

    #endregion

    #region TotalAmount Tests

    [Fact]
    public void TotalAmount_CalculatesCorrectly()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var productNames = CreateProductNames();
        var order = Order.CreateFromCart(cart, productNames);

        // Act
        var total = order.TotalAmount;

        // Assert
        // (2 * 10.00) + (1 * 25.00) = 45.00
        Assert.Equal(45.00m, total.Amount);
        Assert.Equal("USD", total.Currency);
    }

    [Fact]
    public void ItemCount_ReturnsCorrectCount()
    {
        // Arrange
        var cart = CreateCartWithItems();
        var productNames = CreateProductNames();
        var order = Order.CreateFromCart(cart, productNames);

        // Act
        var count = order.ItemCount;

        // Assert
        Assert.Equal(3, count); // 2 + 1
    }

    #endregion

    #region Status Transition Tests

    [Fact]
    public void Confirm_FromPending_TransitionsToConfirmed()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());

        // Act
        order.Confirm();

        // Assert
        Assert.Equal(OrderStatus.Confirmed, order.Status);
        Assert.NotNull(order.ConfirmedAt);
        Assert.NotNull(order.UpdatedAt);
    }

    [Fact]
    public void Confirm_FromNonPending_ThrowsDomainException()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();

        // Act & Assert
        var ex = Assert.Throws<DomainException>(() => order.Confirm());
        Assert.Contains("Cannot confirm order", ex.Message);
    }

    [Fact]
    public void Ship_FromConfirmed_TransitionsToShipped()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();

        // Act
        order.Ship();

        // Assert
        Assert.Equal(OrderStatus.Shipped, order.Status);
        Assert.NotNull(order.ShippedAt);
    }

    [Fact]
    public void Ship_FromPending_ThrowsDomainException()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());

        // Act & Assert
        var ex = Assert.Throws<DomainException>(() => order.Ship());
        Assert.Contains("Cannot ship order", ex.Message);
    }

    [Fact]
    public void Deliver_FromShipped_TransitionsToDelivered()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();
        order.Ship();

        // Act
        order.Deliver();

        // Assert
        Assert.Equal(OrderStatus.Delivered, order.Status);
        Assert.NotNull(order.DeliveredAt);
    }

    [Fact]
    public void Deliver_FromConfirmed_ThrowsDomainException()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();

        // Act & Assert
        var ex = Assert.Throws<DomainException>(() => order.Deliver());
        Assert.Contains("Cannot mark order as delivered", ex.Message);
    }

    #endregion

    #region Cancel Tests

    [Fact]
    public void Cancel_FromPending_TransitionsToCancelled()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());

        // Act
        order.Cancel();

        // Assert
        Assert.Equal(OrderStatus.Cancelled, order.Status);
        Assert.NotNull(order.CancelledAt);
    }

    [Fact]
    public void Cancel_FromConfirmed_TransitionsToCancelled()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();

        // Act
        order.Cancel();

        // Assert
        Assert.Equal(OrderStatus.Cancelled, order.Status);
    }

    [Fact]
    public void Cancel_FromShipped_ThrowsDomainException()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();
        order.Ship();

        // Act & Assert
        var ex = Assert.Throws<DomainException>(() => order.Cancel());
        Assert.Contains("Cannot cancel order", ex.Message);
    }

    [Fact]
    public void Cancel_FromDelivered_ThrowsDomainException()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();
        order.Ship();
        order.Deliver();

        // Act & Assert
        var ex = Assert.Throws<DomainException>(() => order.Cancel());
        Assert.Contains("Cannot cancel order", ex.Message);
    }

    [Fact]
    public void Cancel_AlreadyCancelled_ThrowsDomainException()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Cancel();

        // Act & Assert
        var ex = Assert.Throws<DomainException>(() => order.Cancel());
        Assert.Contains("already cancelled", ex.Message);
    }

    #endregion

    #region UpdateStatus Tests

    [Fact]
    public void UpdateStatus_ToConfirmed_Works()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());

        // Act
        order.UpdateStatus(OrderStatus.Confirmed);

        // Assert
        Assert.Equal(OrderStatus.Confirmed, order.Status);
    }

    [Fact]
    public void UpdateStatus_ToPending_ThrowsDomainException()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();

        // Act & Assert
        var ex = Assert.Throws<DomainException>(() => order.UpdateStatus(OrderStatus.Pending));
        Assert.Contains("Cannot transition back to Pending", ex.Message);
    }

    [Fact]
    public void UpdateStatus_ToSameStatus_DoesNothing()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        var originalUpdatedAt = order.UpdatedAt;

        // Act
        order.UpdateStatus(OrderStatus.Pending);

        // Assert
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Equal(originalUpdatedAt, order.UpdatedAt);
    }

    #endregion

    #region UpdateShippingAddress Tests

    [Fact]
    public void UpdateShippingAddress_WhenPending_UpdatesAddress()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        var newAddress = "456 New St, City, State 54321";

        // Act
        order.UpdateShippingAddress(newAddress);

        // Assert
        Assert.Equal(newAddress, order.ShippingAddress);
        Assert.NotNull(order.UpdatedAt);
    }

    [Fact]
    public void UpdateShippingAddress_WhenConfirmed_UpdatesAddress()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();
        var newAddress = "456 New St, City, State 54321";

        // Act
        order.UpdateShippingAddress(newAddress);

        // Assert
        Assert.Equal(newAddress, order.ShippingAddress);
    }

    [Fact]
    public void UpdateShippingAddress_WhenShipped_ThrowsDomainException()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();
        order.Ship();

        // Act & Assert
        var ex = Assert.Throws<DomainException>(() => order.UpdateShippingAddress("New Address"));
        Assert.Contains("Cannot update shipping address", ex.Message);
    }

    [Fact]
    public void UpdateShippingAddress_WhenCancelled_ThrowsDomainException()
    {
        // Arrange
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Cancel();

        // Act & Assert
        var ex = Assert.Throws<DomainException>(() => order.UpdateShippingAddress("New Address"));
        Assert.Contains("Cannot update a cancelled order", ex.Message);
    }

    #endregion

    #region CanBeCancelled Tests

    [Fact]
    public void CanBeCancelled_WhenPending_ReturnsTrue()
    {
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        Assert.True(order.CanBeCancelled);
    }

    [Fact]
    public void CanBeCancelled_WhenConfirmed_ReturnsTrue()
    {
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();
        Assert.True(order.CanBeCancelled);
    }

    [Fact]
    public void CanBeCancelled_WhenShipped_ReturnsFalse()
    {
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();
        order.Ship();
        Assert.False(order.CanBeCancelled);
    }

    [Fact]
    public void CanBeCancelled_WhenDelivered_ReturnsFalse()
    {
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();
        order.Ship();
        order.Deliver();
        Assert.False(order.CanBeCancelled);
    }

    [Fact]
    public void CanBeCancelled_WhenAlreadyCancelled_ReturnsFalse()
    {
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Cancel();
        Assert.False(order.CanBeCancelled);
    }

    #endregion

    #region CanBeModified Tests

    [Fact]
    public void CanBeModified_WhenPending_ReturnsTrue()
    {
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        Assert.True(order.CanBeModified);
    }

    [Fact]
    public void CanBeModified_WhenDelivered_ReturnsFalse()
    {
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Confirm();
        order.Ship();
        order.Deliver();
        Assert.False(order.CanBeModified);
    }

    [Fact]
    public void CanBeModified_WhenCancelled_ReturnsFalse()
    {
        var order = Order.CreateFromCart(CreateCartWithItems(), CreateProductNames());
        order.Cancel();
        Assert.False(order.CanBeModified);
    }

    #endregion
}
