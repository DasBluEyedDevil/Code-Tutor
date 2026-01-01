using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Domain;

public class ProductTests
{
    [Fact]
    public void Create_WithValidData_ReturnsProduct()
    {
        // Arrange
        var price = Money.Create(29.99m, "USD");

        // Act
        var product = Product.Create("Test Product", "A test product description", price, 1, 10);

        // Assert
        Assert.Equal("Test Product", product.Name);
        Assert.Equal("A test product description", product.Description);
        Assert.Equal(29.99m, product.Price.Amount);
        Assert.Equal("USD", product.Price.Currency);
        Assert.Equal(1, product.CategoryId);
        Assert.Equal(10, product.StockQuantity);
        Assert.True(product.IsActive);
    }

    [Fact]
    public void Create_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var price = Money.Create(29.99m, "USD");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            Product.Create("", "Description", price, 1));
    }

    [Fact]
    public void AddStock_WithPositiveQuantity_IncreasesStock()
    {
        // Arrange
        var price = Money.Create(10m, "USD");
        var product = Product.Create("Product", "Description", price, 1, 5);

        // Act
        product.AddStock(10);

        // Assert
        Assert.Equal(15, product.StockQuantity);
    }

    [Fact]
    public void RemoveStock_WithSufficientStock_DecreasesStock()
    {
        // Arrange
        var price = Money.Create(10m, "USD");
        var product = Product.Create("Product", "Description", price, 1, 10);

        // Act
        product.RemoveStock(5);

        // Assert
        Assert.Equal(5, product.StockQuantity);
    }

    [Fact]
    public void RemoveStock_WithInsufficientStock_ThrowsDomainException()
    {
        // Arrange
        var price = Money.Create(10m, "USD");
        var product = Product.Create("Product", "Description", price, 1, 5);

        // Act & Assert
        Assert.Throws<DomainException>(() => product.RemoveStock(10));
    }

    [Fact]
    public void Deactivate_SetsIsActiveToFalse()
    {
        // Arrange
        var price = Money.Create(10m, "USD");
        var product = Product.Create("Product", "Description", price, 1);

        // Act
        product.Deactivate();

        // Assert
        Assert.False(product.IsActive);
    }
}
