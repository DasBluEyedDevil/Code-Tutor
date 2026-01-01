using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Domain.Entities;

/// <summary>
/// Represents a shopping cart for a user.
/// Aggregate root that manages cart items.
/// </summary>
public class Cart
{
    private readonly List<CartItem> _items = new();

    public int Id { get; private set; }
    public string UserId { get; private set; } = string.Empty;
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // EF Core requires a parameterless constructor
    private Cart() { }

    /// <summary>
    /// Creates a new Cart for the specified user.
    /// </summary>
    /// <param name="userId">The user's unique identifier.</param>
    public static Cart Create(string userId)
    {
        ValidateUserId(userId);

        return new Cart
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Gets the total amount of all items in the cart.
    /// </summary>
    public Money TotalAmount
    {
        get
        {
            if (!_items.Any())
                return Money.Zero();

            var currency = _items.First().UnitPrice.Currency;
            var total = _items.Sum(item => item.Subtotal.Amount);
            return Money.Create(total, currency);
        }
    }

    /// <summary>
    /// Gets the total count of items (sum of all quantities).
    /// </summary>
    public int ItemCount => _items.Sum(item => item.Quantity);

    /// <summary>
    /// Adds an item to the cart or updates quantity if product already exists.
    /// </summary>
    /// <param name="productId">The product ID.</param>
    /// <param name="quantity">The quantity to add (must be positive).</param>
    /// <param name="unitPrice">The unit price at time of addition.</param>
    public void AddItem(int productId, int quantity, Money unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));

        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem is not null)
        {
            existingItem.AddQuantity(quantity);
        }
        else
        {
            var newItem = CartItem.Create(productId, quantity, unitPrice);
            _items.Add(newItem);
        }

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Removes an item from the cart.
    /// </summary>
    /// <param name="productId">The product ID to remove.</param>
    /// <exception cref="DomainException">Thrown when product is not in cart.</exception>
    public void RemoveItem(int productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId)
            ?? throw new DomainException($"Product with ID '{productId}' is not in the cart.");

        _items.Remove(item);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the quantity of an item in the cart.
    /// If quantity is 0, the item is removed.
    /// </summary>
    /// <param name="productId">The product ID.</param>
    /// <param name="quantity">The new quantity.</param>
    /// <exception cref="DomainException">Thrown when product is not in cart.</exception>
    public void UpdateItemQuantity(int productId, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId)
            ?? throw new DomainException($"Product with ID '{productId}' is not in the cart.");

        if (quantity <= 0)
        {
            _items.Remove(item);
        }
        else
        {
            item.UpdateQuantity(quantity);
        }

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Removes all items from the cart.
    /// </summary>
    public void Clear()
    {
        _items.Clear();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the cart contains a specific product.
    /// </summary>
    /// <param name="productId">The product ID to check.</param>
    /// <returns>True if the product is in the cart; otherwise, false.</returns>
    public bool ContainsProduct(int productId)
    {
        return _items.Any(i => i.ProductId == productId);
    }

    /// <summary>
    /// Gets a specific item from the cart by product ID.
    /// </summary>
    /// <param name="productId">The product ID.</param>
    /// <returns>The cart item if found; otherwise, null.</returns>
    public CartItem? GetItem(int productId)
    {
        return _items.FirstOrDefault(i => i.ProductId == productId);
    }

    private static void ValidateUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID is required.", nameof(userId));
    }
}
