using ShopFlow.Domain.Enums;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Domain.Entities;

/// <summary>
/// Represents a customer order.
/// Aggregate root that manages order items and status workflow.
/// </summary>
public class Order
{
    private readonly List<OrderItem> _items = new();

    public int Id { get; private set; }
    public string UserId { get; private set; } = string.Empty;
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public string? ShippingAddress { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }
    public DateTime? ShippedAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }
    public DateTime? CancelledAt { get; private set; }

    // EF Core requires a parameterless constructor
    private Order() { }

    /// <summary>
    /// Creates a new Order from a Cart.
    /// Copies cart items as order items (snapshots).
    /// </summary>
    /// <param name="cart">The cart to create the order from.</param>
    /// <param name="productNames">Dictionary mapping ProductId to ProductName for snapshot.</param>
    /// <param name="shippingAddress">The shipping address for the order.</param>
    /// <exception cref="DomainException">Thrown when the cart is empty.</exception>
    public static Order CreateFromCart(Cart cart, IDictionary<int, string> productNames, string? shippingAddress = null)
    {
        ValidateUserId(cart.UserId);

        if (!cart.Items.Any())
            throw new DomainException("Cannot create an order from an empty cart.");

        var order = new Order
        {
            UserId = cart.UserId,
            Status = OrderStatus.Pending,
            ShippingAddress = shippingAddress,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var cartItem in cart.Items)
        {
            if (!productNames.TryGetValue(cartItem.ProductId, out var productName))
            {
                throw new DomainException($"Product name not found for product ID {cartItem.ProductId}.");
            }

            var orderItem = OrderItem.CreateFromCartItem(cartItem, productName);
            order._items.Add(orderItem);
        }

        return order;
    }

    /// <summary>
    /// Gets the total amount of all items in the order.
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
    /// Confirms the order. Transitions from Pending to Confirmed.
    /// </summary>
    /// <exception cref="DomainException">Thrown when the order cannot be confirmed.</exception>
    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException($"Cannot confirm order. Current status is '{Status}'. Only pending orders can be confirmed.");

        Status = OrderStatus.Confirmed;
        ConfirmedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the order as shipped. Transitions from Confirmed to Shipped.
    /// </summary>
    /// <exception cref="DomainException">Thrown when the order cannot be shipped.</exception>
    public void Ship()
    {
        if (Status != OrderStatus.Confirmed)
            throw new DomainException($"Cannot ship order. Current status is '{Status}'. Only confirmed orders can be shipped.");

        Status = OrderStatus.Shipped;
        ShippedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the order as delivered. Transitions from Shipped to Delivered.
    /// </summary>
    /// <exception cref="DomainException">Thrown when the order cannot be marked as delivered.</exception>
    public void Deliver()
    {
        if (Status != OrderStatus.Shipped)
            throw new DomainException($"Cannot mark order as delivered. Current status is '{Status}'. Only shipped orders can be delivered.");

        Status = OrderStatus.Delivered;
        DeliveredAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cancels the order. Only allowed if order has not been shipped yet.
    /// </summary>
    /// <exception cref="DomainException">Thrown when the order cannot be cancelled.</exception>
    public void Cancel()
    {
        if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
            throw new DomainException($"Cannot cancel order. Current status is '{Status}'. Orders that have been shipped or delivered cannot be cancelled.");

        if (Status == OrderStatus.Cancelled)
            throw new DomainException("Order is already cancelled.");

        Status = OrderStatus.Cancelled;
        CancelledAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the order status to a new value.
    /// Uses the appropriate transition method based on the target status.
    /// </summary>
    /// <param name="newStatus">The new status to transition to.</param>
    /// <exception cref="DomainException">Thrown when the transition is not allowed.</exception>
    public void UpdateStatus(OrderStatus newStatus)
    {
        if (newStatus == Status)
            return;

        switch (newStatus)
        {
            case OrderStatus.Confirmed:
                Confirm();
                break;
            case OrderStatus.Shipped:
                Ship();
                break;
            case OrderStatus.Delivered:
                Deliver();
                break;
            case OrderStatus.Cancelled:
                Cancel();
                break;
            case OrderStatus.Pending:
                throw new DomainException("Cannot transition back to Pending status.");
            default:
                throw new DomainException($"Unknown order status: {newStatus}");
        }
    }

    /// <summary>
    /// Updates the shipping address.
    /// Only allowed if the order has not been shipped yet.
    /// </summary>
    /// <param name="shippingAddress">The new shipping address.</param>
    /// <exception cref="DomainException">Thrown when the address cannot be updated.</exception>
    public void UpdateShippingAddress(string shippingAddress)
    {
        if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
            throw new DomainException("Cannot update shipping address after order has been shipped.");

        if (Status == OrderStatus.Cancelled)
            throw new DomainException("Cannot update a cancelled order.");

        ShippingAddress = shippingAddress;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the order can be cancelled.
    /// </summary>
    public bool CanBeCancelled => Status != OrderStatus.Shipped &&
                                   Status != OrderStatus.Delivered &&
                                   Status != OrderStatus.Cancelled;

    /// <summary>
    /// Checks if the order can be modified (status or address changes).
    /// </summary>
    public bool CanBeModified => Status != OrderStatus.Delivered &&
                                  Status != OrderStatus.Cancelled;

    private static void ValidateUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID is required.", nameof(userId));
    }
}
