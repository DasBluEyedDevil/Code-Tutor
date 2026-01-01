using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Domain.Entities;

/// <summary>
/// Represents an item in an order.
/// Contains a snapshot of the product at the time of purchase.
/// </summary>
public class OrderItem
{
    public int Id { get; private set; }
    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = null!;

    // Navigation property
    public Order? Order { get; private set; }

    // EF Core requires a parameterless constructor
    private OrderItem() { }

    /// <summary>
    /// Creates a new OrderItem with the specified details.
    /// This is a snapshot of the product at purchase time.
    /// </summary>
    /// <param name="productId">The product ID.</param>
    /// <param name="productName">The product name at time of purchase.</param>
    /// <param name="quantity">The quantity ordered.</param>
    /// <param name="unitPrice">The unit price at time of purchase.</param>
    internal static OrderItem Create(int productId, string productName, int quantity, Money unitPrice)
    {
        ValidateProductId(productId);
        ValidateProductName(productName);
        ValidateQuantity(quantity);

        return new OrderItem
        {
            ProductId = productId,
            ProductName = productName,
            Quantity = quantity,
            UnitPrice = unitPrice
        };
    }

    /// <summary>
    /// Creates an OrderItem from a CartItem, capturing product details as a snapshot.
    /// </summary>
    /// <param name="cartItem">The cart item to create from.</param>
    /// <param name="productName">The product name to snapshot.</param>
    internal static OrderItem CreateFromCartItem(CartItem cartItem, string productName)
    {
        return Create(
            cartItem.ProductId,
            productName,
            cartItem.Quantity,
            cartItem.UnitPrice
        );
    }

    /// <summary>
    /// Gets the subtotal for this item (quantity * unit price).
    /// </summary>
    public Money Subtotal => UnitPrice.Multiply(Quantity);

    private static void ValidateProductId(int productId)
    {
        if (productId <= 0)
            throw new ArgumentException("Product ID must be positive.", nameof(productId));
    }

    private static void ValidateProductName(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name is required.", nameof(productName));
    }

    private static void ValidateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));
    }
}
