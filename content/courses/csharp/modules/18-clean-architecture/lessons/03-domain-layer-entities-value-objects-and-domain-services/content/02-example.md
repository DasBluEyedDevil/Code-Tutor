---
type: "EXAMPLE"
title: "ShopFlow Domain Layer Code"
---

Here's the complete Domain layer implementation for ShopFlow, demonstrating proper Entity and Value Object design with rich domain behavior.

```csharp
// ===== SHOPFLOW DOMAIN LAYER =====

// ========== VALUE OBJECTS ==========

// Value Object: Money (immutable, value equality)
namespace ShopFlow.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new DomainException("Money amount cannot be negative");
        
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            throw new DomainException("Currency must be a 3-letter ISO code");

        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    // Factory method for USD
    public static Money USD(decimal amount) => new(amount, "USD");
    
    // Factory method for zero
    public static Money Zero(string currency) => new(0, currency);

    // Rich behavior - Value Objects can have methods!
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new DomainException($"Cannot add {Currency} to {other.Currency}");
        
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new DomainException($"Cannot subtract {other.Currency} from {Currency}");
        
        if (Amount < other.Amount)
            throw new DomainException("Result would be negative");
        
        return new Money(Amount - other.Amount, Currency);
    }

    public Money Multiply(int quantity)
    {
        if (quantity < 0)
            throw new DomainException("Quantity cannot be negative");
        
        return new Money(Amount * quantity, Currency);
    }

    public override string ToString() => $"{Currency} {Amount:N2}";
}


// Value Object: Address (immutable, value equality)
namespace ShopFlow.Domain.ValueObjects;

public sealed record Address
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }
    public string Country { get; }

    public Address(
        string street,
        string city,
        string state,
        string postalCode,
        string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new DomainException("Street is required");
        if (string.IsNullOrWhiteSpace(city))
            throw new DomainException("City is required");
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new DomainException("Postal code is required");
        if (string.IsNullOrWhiteSpace(country))
            throw new DomainException("Country is required");

        Street = street.Trim();
        City = city.Trim();
        State = state?.Trim() ?? string.Empty;
        PostalCode = postalCode.Trim();
        Country = country.Trim();
    }

    public string FullAddress => $"{Street}, {City}, {State} {PostalCode}, {Country}";
}


// ========== ENTITIES ==========

// Entity: Product (has identity, has behavior)
namespace ShopFlow.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Money Price { get; private set; }
    public int StockQuantity { get; private set; }
    public ProductStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastModifiedAt { get; private set; }

    // Private constructor for EF Core
    private Product() { }

    // Factory method - ensures valid creation
    public static Product Create(
        string name,
        string description,
        Money price,
        int initialStock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name is required");
        
        if (name.Length > 200)
            throw new DomainException("Product name cannot exceed 200 characters");
        
        if (initialStock < 0)
            throw new DomainException("Initial stock cannot be negative");

        return new Product
        {
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            Price = price ?? throw new DomainException("Price is required"),
            StockQuantity = initialStock,
            Status = ProductStatus.Active,
            CreatedAt = DateTime.UtcNow
        };
    }

    // Rich domain behavior - not just getters/setters!
    public void UpdatePrice(Money newPrice)
    {
        Price = newPrice ?? throw new DomainException("Price cannot be null");
        LastModifiedAt = DateTime.UtcNow;
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity to add must be positive");
        
        StockQuantity += quantity;
        
        // Reactivate if was out of stock
        if (Status == ProductStatus.OutOfStock)
            Status = ProductStatus.Active;
        
        LastModifiedAt = DateTime.UtcNow;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity to remove must be positive");
        
        if (quantity > StockQuantity)
            throw new InsufficientStockException(Id, StockQuantity, quantity);
        
        StockQuantity -= quantity;
        
        if (StockQuantity == 0)
            Status = ProductStatus.OutOfStock;
        
        LastModifiedAt = DateTime.UtcNow;
    }

    public void Discontinue()
    {
        if (Status == ProductStatus.Discontinued)
            throw new DomainException("Product is already discontinued");
        
        Status = ProductStatus.Discontinued;
        LastModifiedAt = DateTime.UtcNow;
    }

    public bool IsAvailable => Status == ProductStatus.Active && StockQuantity > 0;
}


// ========== REPOSITORY INTERFACE ==========
// Defined in Domain or Application layer (we'll use Application)

namespace ShopFlow.Application.Products;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetActiveProductsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}


// ========== DOMAIN EXCEPTIONS ==========

namespace ShopFlow.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

public class InsufficientStockException : DomainException
{
    public int ProductId { get; }
    public int AvailableStock { get; }
    public int RequestedQuantity { get; }

    public InsufficientStockException(int productId, int available, int requested)
        : base($"Product {productId} has only {available} in stock, but {requested} was requested")
    {
        ProductId = productId;
        AvailableStock = available;
        RequestedQuantity = requested;
    }
}


// ========== ENUMS ==========

namespace ShopFlow.Domain.Enums;

public enum ProductStatus
{
    Active,
    OutOfStock,
    Discontinued
}
```
