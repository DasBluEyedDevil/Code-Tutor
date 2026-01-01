---
type: "EXAMPLE"
title: "TDD in Action: ShopFlow Cart Total"
---

Let's build a shopping cart calculator using TDD. We'll go through each step of Red-Green-Refactor.

```csharp
using Xunit;

// ===== STEP 1: RED - Write a Failing Test =====
// We want a CartCalculator that calculates totals
// Start by describing what we WANT, not what exists!

public class CartCalculatorTests
{
    [Fact]
    public void CalculateTotal_SingleItem_ReturnsItemPrice()
    {
        // Arrange
        var calculator = new CartCalculator();
        var items = new List<CartItem>
        {
            new CartItem("Widget", 10.00m, 1)
        };
        
        // Act
        decimal total = calculator.CalculateTotal(items);
        
        // Assert
        Assert.Equal(10.00m, total);
    }
    // This test FAILS - CartCalculator doesn't exist yet! (RED)
}

// ===== STEP 2: GREEN - Minimal Implementation =====
// Write JUST ENOUGH code to pass the test

public record CartItem(string Name, decimal Price, int Quantity);

public class CartCalculator
{
    public decimal CalculateTotal(List<CartItem> items)
    {
        // Simplest thing that works:
        return items.Sum(i => i.Price * i.Quantity);
    }
}
// Test now PASSES! (GREEN)

// ===== STEP 3: Add More Tests, Repeat Cycle =====

public class CartCalculatorTests_Extended
{
    private readonly CartCalculator _calculator = new();
    
    [Fact]
    public void CalculateTotal_MultipleItems_ReturnsSumOfAll()
    {
        var items = new List<CartItem>
        {
            new CartItem("Widget", 10.00m, 2),   // 20.00
            new CartItem("Gadget", 25.00m, 1)    // 25.00
        };
        
        Assert.Equal(45.00m, _calculator.CalculateTotal(items));
    }
    
    [Fact]
    public void CalculateTotal_EmptyCart_ReturnsZero()
    {
        var items = new List<CartItem>();
        Assert.Equal(0m, _calculator.CalculateTotal(items));
    }
    
    [Fact]
    public void CalculateTotal_WithDiscount_AppliesDiscount()
    {
        // RED: This test fails - no discount support yet!
        var items = new List<CartItem>
        {
            new CartItem("Widget", 100.00m, 1)
        };
        
        decimal total = _calculator.CalculateTotal(items, discountPercent: 10);
        
        Assert.Equal(90.00m, total); // 100 - 10% = 90
    }
}

// ===== GREEN: Add discount support =====
public class CartCalculatorWithDiscount
{
    public decimal CalculateTotal(List<CartItem> items, decimal discountPercent = 0)
    {
        decimal subtotal = items.Sum(i => i.Price * i.Quantity);
        decimal discount = subtotal * (discountPercent / 100);
        return subtotal - discount;
    }
}

// ===== REFACTOR: Clean up the code =====
public class CartCalculatorRefactored
{
    public decimal CalculateTotal(List<CartItem> items, decimal discountPercent = 0)
    {
        decimal subtotal = CalculateSubtotal(items);
        return ApplyDiscount(subtotal, discountPercent);
    }
    
    private decimal CalculateSubtotal(List<CartItem> items)
        => items.Sum(i => i.Price * i.Quantity);
    
    private decimal ApplyDiscount(decimal amount, decimal percent)
        => amount * (1 - percent / 100);
}
// Tests still pass! Safe refactoring.

Console.WriteLine("TDD Cycle Complete!");
Console.WriteLine("1. RED: Write failing test");
Console.WriteLine("2. GREEN: Make it pass (minimal code)");
Console.WriteLine("3. REFACTOR: Clean up (tests protect you)");
```
