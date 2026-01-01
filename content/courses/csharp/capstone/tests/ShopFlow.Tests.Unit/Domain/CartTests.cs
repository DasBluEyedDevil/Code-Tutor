using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Domain;

public class CartTests
{
    [Fact]
    public void Create_WithValidUserId_ReturnsCart()
    {
        // Arrange
        var userId = "user-123";

        // Act
        var cart = Cart.Create(userId);

        // Assert
        Assert.Equal(userId, cart.UserId);
        Assert.Empty(cart.Items);
        Assert.Equal(Money.Zero(), cart.TotalAmount);
        Assert.NotEqual(default, cart.CreatedAt);
    }

    [Fact]
    public void Create_WithEmptyUserId_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Cart.Create(""));
    }

    [Fact]
    public void Create_WithNullUserId_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Cart.Create(null!));
    }

    [Fact]
    public void AddItem_WithValidData_AddsItemToCart()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        var unitPrice = Money.Create(29.99m, "USD");

        // Act
        cart.AddItem(1, 2, unitPrice);

        // Assert
        Assert.Single(cart.Items);
        var item = cart.Items.First();
        Assert.Equal(1, item.ProductId);
        Assert.Equal(2, item.Quantity);
        Assert.Equal(unitPrice, item.UnitPrice);
    }

    [Fact]
    public void AddItem_WithExistingProduct_UpdatesQuantity()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        var unitPrice = Money.Create(29.99m, "USD");
        cart.AddItem(1, 2, unitPrice);

        // Act
        cart.AddItem(1, 3, unitPrice);

        // Assert
        Assert.Single(cart.Items);
        var item = cart.Items.First();
        Assert.Equal(5, item.Quantity);
    }

    [Fact]
    public void AddItem_WithZeroQuantity_ThrowsArgumentException()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        var unitPrice = Money.Create(29.99m, "USD");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => cart.AddItem(1, 0, unitPrice));
    }

    [Fact]
    public void AddItem_WithNegativeQuantity_ThrowsArgumentException()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        var unitPrice = Money.Create(29.99m, "USD");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => cart.AddItem(1, -1, unitPrice));
    }

    [Fact]
    public void RemoveItem_WithExistingProduct_RemovesItem()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(29.99m, "USD"));
        cart.AddItem(2, 1, Money.Create(19.99m, "USD"));

        // Act
        cart.RemoveItem(1);

        // Assert
        Assert.Single(cart.Items);
        Assert.Equal(2, cart.Items.First().ProductId);
    }

    [Fact]
    public void RemoveItem_WithNonExistingProduct_ThrowsDomainException()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(29.99m, "USD"));

        // Act & Assert
        Assert.Throws<DomainException>(() => cart.RemoveItem(99));
    }

    [Fact]
    public void UpdateItemQuantity_WithValidQuantity_UpdatesQuantity()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(29.99m, "USD"));

        // Act
        cart.UpdateItemQuantity(1, 5);

        // Assert
        Assert.Equal(5, cart.Items.First().Quantity);
    }

    [Fact]
    public void UpdateItemQuantity_WithZeroQuantity_RemovesItem()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(29.99m, "USD"));

        // Act
        cart.UpdateItemQuantity(1, 0);

        // Assert
        Assert.Empty(cart.Items);
    }

    [Fact]
    public void UpdateItemQuantity_WithNonExistingProduct_ThrowsDomainException()
    {
        // Arrange
        var cart = Cart.Create("user-123");

        // Act & Assert
        Assert.Throws<DomainException>(() => cart.UpdateItemQuantity(99, 5));
    }

    [Fact]
    public void Clear_RemovesAllItems()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(29.99m, "USD"));
        cart.AddItem(2, 1, Money.Create(19.99m, "USD"));

        // Act
        cart.Clear();

        // Assert
        Assert.Empty(cart.Items);
    }

    [Fact]
    public void TotalAmount_CalculatesCorrectly()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(10.00m, "USD")); // 20.00
        cart.AddItem(2, 3, Money.Create(5.00m, "USD"));  // 15.00

        // Act
        var total = cart.TotalAmount;

        // Assert
        Assert.Equal(35.00m, total.Amount);
        Assert.Equal("USD", total.Currency);
    }

    [Fact]
    public void TotalAmount_WithEmptyCart_ReturnsZero()
    {
        // Arrange
        var cart = Cart.Create("user-123");

        // Act
        var total = cart.TotalAmount;

        // Assert
        Assert.Equal(0m, total.Amount);
    }

    [Fact]
    public void ItemCount_ReturnsCorrectCount()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(10.00m, "USD"));
        cart.AddItem(2, 3, Money.Create(5.00m, "USD"));

        // Act
        var count = cart.ItemCount;

        // Assert
        Assert.Equal(5, count);
    }

    [Fact]
    public void ContainsProduct_WithExistingProduct_ReturnsTrue()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(10.00m, "USD"));

        // Act
        var contains = cart.ContainsProduct(1);

        // Assert
        Assert.True(contains);
    }

    [Fact]
    public void ContainsProduct_WithNonExistingProduct_ReturnsFalse()
    {
        // Arrange
        var cart = Cart.Create("user-123");
        cart.AddItem(1, 2, Money.Create(10.00m, "USD"));

        // Act
        var contains = cart.ContainsProduct(99);

        // Assert
        Assert.False(contains);
    }
}
