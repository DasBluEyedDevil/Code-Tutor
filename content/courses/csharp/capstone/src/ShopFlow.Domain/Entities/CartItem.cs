using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Domain.Entities;

/// <summary>
/// Represents an item in a shopping cart.
/// Contains product reference, quantity, and unit price at time of addition.
/// </summary>
public class CartItem
{
    public int Id { get; private set; }
    public int CartId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = null!;
    public DateTime AddedAt { get; private set; }

    // Navigation property
    public Cart? Cart { get; private set; }

    // EF Core requires a parameterless constructor
    private CartItem() { }

    /// <summary>
    /// Creates a new CartItem with the specified details.
    /// </summary>
    internal static CartItem Create(int productId, int quantity, Money unitPrice)
    {
        ValidateProductId(productId);
        ValidateQuantity(quantity);

        return new CartItem
        {
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = unitPrice,
            AddedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Gets the subtotal for this item (quantity * unit price).
    /// </summary>
    public Money Subtotal => UnitPrice.Multiply(Quantity);

    /// <summary>
    /// Updates the quantity of this item.
    /// </summary>
    internal void UpdateQuantity(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));

        Quantity = quantity;
    }

    /// <summary>
    /// Adds to the existing quantity.
    /// </summary>
    internal void AddQuantity(int quantity)
    {
        ValidateQuantity(quantity);
        Quantity += quantity;
    }

    private static void ValidateProductId(int productId)
    {
        if (productId <= 0)
            throw new ArgumentException("Product ID must be positive.", nameof(productId));
    }

    private static void ValidateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));
    }
}
