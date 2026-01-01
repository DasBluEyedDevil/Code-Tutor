using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Domain;

public class MoneyTests
{
    [Fact]
    public void Create_WithValidAmount_ReturnsMoney()
    {
        // Act
        var money = Money.Create(100.50m, "USD");

        // Assert
        Assert.Equal(100.50m, money.Amount);
        Assert.Equal("USD", money.Currency);
    }

    [Fact]
    public void Create_WithNegativeAmount_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Money.Create(-10m, "USD"));
    }

    [Fact]
    public void Create_RoundsToTwoDecimalPlaces()
    {
        // Act
        var money = Money.Create(10.999m, "USD");

        // Assert
        Assert.Equal(11.00m, money.Amount);
    }

    [Fact]
    public void Add_WithSameCurrency_ReturnsSum()
    {
        // Arrange
        var money1 = Money.Create(10m, "USD");
        var money2 = Money.Create(20m, "USD");

        // Act
        var result = money1.Add(money2);

        // Assert
        Assert.Equal(30m, result.Amount);
        Assert.Equal("USD", result.Currency);
    }

    [Fact]
    public void Add_WithDifferentCurrency_ThrowsInvalidOperationException()
    {
        // Arrange
        var money1 = Money.Create(10m, "USD");
        var money2 = Money.Create(20m, "EUR");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => money1.Add(money2));
    }

    [Fact]
    public void Equals_WithSameAmountAndCurrency_ReturnsTrue()
    {
        // Arrange
        var money1 = Money.Create(100m, "USD");
        var money2 = Money.Create(100m, "USD");

        // Assert
        Assert.True(money1.Equals(money2));
        Assert.True(money1 == money2);
    }

    [Fact]
    public void Multiply_WithPositiveFactor_ReturnsProduct()
    {
        // Arrange
        var money = Money.Create(10m, "USD");

        // Act
        var result = money.Multiply(3);

        // Assert
        Assert.Equal(30m, result.Amount);
    }
}
