---
type: "REAL_WORLD"
title: "Method Design in Production Code"
---

In professional e-commerce applications like ShopFlow, well-designed methods are the foundation of maintainable code. Here are the key principles production teams follow when writing methods:

**Single Responsibility Principle**: Each method should do ONE thing well. A method called `ProcessOrder` should only process the order, not also send emails, update inventory, and log analytics. If your method name needs 'and' in it, split it into multiple methods. In ShopFlow, we have separate `ValidateOrder()`, `CalculateTotal()`, `ApplyDiscount()`, and `SaveOrder()` methods instead of one massive `DoEverything()` method.

**Method Length Guidance**: Keep methods under 30 lines. If a method grows beyond this, it likely does too much and should be split. Long methods are hard to test, debug, and understand. When you find yourself scrolling to understand a method, refactor it.

**Parameter Count**: More than 3-4 parameters is a code smell. When you need many parameters, create a parameter object:

```csharp
// BAD: Too many parameters
public void CreateOrder(string customerName, string email, string address, string city, string zip, List<Product> items, decimal discount, string couponCode) { }

// GOOD: Use a parameter object
public void CreateOrder(OrderRequest request) { }

public class OrderRequest
{
    public Customer Customer { get; set; }
    public List<Product> Items { get; set; }
    public DiscountInfo Discount { get; set; }
}
```

**Guard Clauses (Return Early)**: Instead of deeply nested if-else blocks, validate preconditions at the start and return early. This makes code much more readable:

```csharp
// BAD: Deep nesting
public decimal CalculateDiscount(Order order)
{
    if (order != null)
    {
        if (order.Items.Count > 0)
        {
            if (order.Customer.IsPremium)
            {
                return order.Total * 0.2m;
            }
        }
    }
    return 0;
}

// GOOD: Guard clauses
public decimal CalculateDiscount(Order order)
{
    if (order == null) return 0;
    if (order.Items.Count == 0) return 0;
    if (!order.Customer.IsPremium) return 0;
    
    return order.Total * 0.2m;
}
```

**XML Documentation for Public APIs**: In ShopFlow, all public methods have XML documentation that describes purpose, parameters, return values, and exceptions. This generates IntelliSense tooltips and API documentation automatically:

```csharp
/// <summary>
/// Applies a promotional discount to the order.
/// </summary>
/// <param name="order">The order to discount.</param>
/// <param name="promoCode">The promotional code to apply.</param>
/// <returns>The discount amount applied.</returns>
/// <exception cref="InvalidPromoCodeException">Thrown when promo code is expired or invalid.</exception>
public decimal ApplyPromotion(Order order, string promoCode) { }
```

These patterns keep ShopFlow maintainable as it scales. Start practicing them now!