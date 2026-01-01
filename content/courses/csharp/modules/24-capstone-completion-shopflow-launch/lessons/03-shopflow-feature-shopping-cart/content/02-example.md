---
type: "EXAMPLE"
title: "Cart Domain Model"
---

The Cart entity encapsulates all business rules about shopping cart behavior. Items are managed through the Cart, ensuring invariants like minimum quantities and maximum items are enforced at the domain level.

```csharp
// Domain/Entities/Cart.cs

using ShopFlow.Domain.Common;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Domain.Entities;

public class Cart : Entity<int>, IAggregateRoot
{
    private readonly List<CartItem> _items = new();
    
    public int? UserId { get; private set; }
    public string? SessionId { get; private set; }
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    // Business constants
    public const int MaxItemsPerCart = 50;
    public const int MaxQuantityPerItem = 99;

    private Cart() { } // EF Core constructor

    public static Cart CreateForUser(int userId)
    {
        return new Cart
        {
            UserId = userId,
            SessionId = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static Cart CreateForSession(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException("Session ID cannot be empty", nameof(sessionId));

        return new Cart
        {
            UserId = null,
            SessionId = sessionId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void AddItem(Product product, int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be positive");

        if (quantity > MaxQuantityPerItem)
            throw new DomainException($"Cannot add more than {MaxQuantityPerItem} of an item");

        var existingItem = _items.FirstOrDefault(i => i.ProductId == product.Id);

        if (existingItem is not null)
        {
            var newQuantity = existingItem.Quantity + quantity;
            if (newQuantity > MaxQuantityPerItem)
                throw new DomainException($"Cannot have more than {MaxQuantityPerItem} of an item");
            
            existingItem.UpdateQuantity(newQuantity);
        }
        else
        {
            if (_items.Count >= MaxItemsPerCart)
                throw new DomainException($"Cannot have more than {MaxItemsPerCart} items in cart");

            var item = CartItem.Create(this, product, quantity);
            _items.Add(item);
        }

        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateItemQuantity(int productId, int newQuantity)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId)
            ?? throw new DomainException($"Product {productId} not in cart");

        if (newQuantity <= 0)
        {
            _items.Remove(item);
        }
        else if (newQuantity > MaxQuantityPerItem)
        {
            throw new DomainException($"Cannot have more than {MaxQuantityPerItem} of an item");
        }
        else
        {
            item.UpdateQuantity(newQuantity);
        }

        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveItem(int productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
        {
            _items.Remove(item);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Clear()
    {
        _items.Clear();
        UpdatedAt = DateTime.UtcNow;
    }

    public Money GetSubtotal()
    {
        if (!_items.Any())
            return Money.Zero("USD");

        return _items
            .Select(i => i.GetLineTotal())
            .Aggregate((a, b) => a.Add(b));
    }

    public void AssociateWithUser(int userId)
    {
        UserId = userId;
        SessionId = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MergeFrom(Cart other)
    {
        foreach (var otherItem in other.Items)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductId == otherItem.ProductId);
            
            if (existingItem is not null)
            {
                // Take the larger quantity when merging
                var newQuantity = Math.Max(existingItem.Quantity, otherItem.Quantity);
                existingItem.UpdateQuantity(Math.Min(newQuantity, MaxQuantityPerItem));
            }
            else if (_items.Count < MaxItemsPerCart)
            {
                var newItem = CartItem.Create(this, otherItem.Product, otherItem.Quantity);
                _items.Add(newItem);
            }
        }

        UpdatedAt = DateTime.UtcNow;
    }
}

// Domain/Entities/CartItem.cs

namespace ShopFlow.Domain.Entities;

public class CartItem : Entity<int>
{
    public int CartId { get; private set; }
    public Cart Cart { get; private set; } = null!;
    public int ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = null!;
    public DateTime AddedAt { get; private set; }

    private CartItem() { }

    internal static CartItem Create(Cart cart, Product product, int quantity)
    {
        return new CartItem
        {
            Cart = cart,
            CartId = cart.Id,
            Product = product,
            ProductId = product.Id,
            Quantity = quantity,
            UnitPrice = product.Price,
            AddedAt = DateTime.UtcNow
        };
    }

    internal void UpdateQuantity(int newQuantity)
    {
        Quantity = newQuantity;
    }

    public Money GetLineTotal()
    {
        return UnitPrice.Multiply(Quantity);
    }
}
```
