---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// STEP 1: Install xUnit packages
// dotnet add package xunit
// dotnet add package xunit.runner.visualstudio
// dotnet add package Microsoft.NET.Test.Sdk

using Xunit;

// ===== THE CODE TO TEST =====
public class Calculator
{
    public int Add(int a, int b) => a + b;
    public int Subtract(int a, int b) => a - b;
    public int Divide(int a, int b)
    {
        if (b == 0) throw new DivideByZeroException();
        return a / b;
    }
}

// ===== THE TESTS =====
public class CalculatorTests
{
    // [Fact] = A single test case
    [Fact]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        // ARRANGE
        var calculator = new Calculator();
        
        // ACT
        int result = calculator.Add(3, 5);
        
        // ASSERT
        Assert.Equal(8, result);
    }
    
    [Fact]
    public void Add_NegativeNumbers_ReturnsCorrectSum()
    {
        var calculator = new Calculator();
        Assert.Equal(-8, calculator.Add(-3, -5));
    }
    
    // [Theory] + [InlineData] = Parameterized tests
    [Theory]
    [InlineData(10, 5, 5)]
    [InlineData(100, 25, 75)]
    [InlineData(0, 0, 0)]
    public void Subtract_VariousInputs_ReturnsCorrectDifference(
        int a, int b, int expected)
    {
        var calculator = new Calculator();
        Assert.Equal(expected, calculator.Subtract(a, b));
    }
    
    // Testing exceptions
    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        var calculator = new Calculator();
        
        // Assert.Throws checks that an exception is thrown
        Assert.Throws<DivideByZeroException>(
            () => calculator.Divide(10, 0)
        );
    }
}

// ===== COMMON ASSERTIONS (xUnit 2.9+) =====
// Assert.Equal(expected, actual)      - Values match
// Assert.NotEqual(expected, actual)   - Values don't match
// Assert.True(condition)              - Condition is true
// Assert.False(condition)             - Condition is false
// Assert.Null(object)                 - Object is null
// Assert.NotNull(object)              - Object is not null
// Assert.Throws<T>(() => code)        - Code throws exception
// Assert.ThrowsAsync<T>(async () => ) - Async exception test
// Assert.Contains(item, collection)   - Collection contains item
// Assert.Empty(collection)            - Collection is empty
// Assert.Single(collection)           - Collection has exactly one item
// Assert.All(collection, predicate)   - All items match predicate
// Assert.Equivalent(expected, actual) - Deep object comparison (xUnit 2.5+)
// Assert.Multiple(() => {}, () => {}) - Run multiple asserts, report all failures

// Run tests: dotnet test
Console.WriteLine("Run tests with: dotnet test");
```
