---
type: "EXAMPLE"
title: "Order Domain Model"
---

The Order entity captures a complete snapshot of the purchase at the moment it was placed. Notice how we copy product information rather than referencing it, ensuring the order remains accurate even if products change later.

```csharp
// Domain/Entities/Order.cs

using ShopFlow.Domain.Common;
using ShopFlow.Domain.Events;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Domain.Entities;

public class Order : Entity<int>, IAggregateRoot
{
    private readonly List<OrderItem> _items = new();
    private readonly List<OrderStatusHistory> _statusHistory = new();

    public int UserId { get; private set; }
    public string OrderNumber { get; private set; } = null!;
    public OrderStatus Status { get; private set; }
    public Address ShippingAddress { get; private set; } = null!;
    public Address? BillingAddress { get; private set; }
    public Money Subtotal { get; private set; } = null!;
    public Money ShippingCost { get; private set; } = null!;
    public Money Tax { get; private set; } = null!;
    public Money Total { get; private set; } = null!;
    public string? PaymentIntentId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ShippedAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }
    public string? TrackingNumber { get; private set; }
    public string? Notes { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public IReadOnlyCollection<OrderStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    private Order() { } // EF Core constructor

    public static Order CreateFromCart(
        Cart cart,
        int userId,
        Address shippingAddress,
        Address? billingAddress = null)
    {
        if (!cart.Items.Any())
            throw new DomainException("Cannot create order from empty cart");

        var order = new Order
        {
            UserId = userId,
            OrderNumber = GenerateOrderNumber(),
            Status = OrderStatus.Pending,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress ?? shippingAddress,
            CreatedAt = DateTime.UtcNow
        };

        // Copy cart items as order items (snapshot at time of order)
        foreach (var cartItem in cart.Items)
        {
            var orderItem = OrderItem.CreateFromCartItem(cartItem);
            order._items.Add(orderItem);
        }

        // Calculate totals
        order.Subtotal = order.CalculateSubtotal();
        order.ShippingCost = order.CalculateShipping();
        order.Tax = order.CalculateTax();
        order.Total = order.Subtotal.Add(order.ShippingCost).Add(order.Tax);

        // Record initial status
        order._statusHistory.Add(new OrderStatusHistory(
            OrderStatus.Pending,
            DateTime.UtcNow,
            "Order created"));

        // Raise domain event
        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

    public void Confirm(string paymentIntentId)
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException($"Cannot confirm order in {Status} status");

        PaymentIntentId = paymentIntentId;
        TransitionTo(OrderStatus.Confirmed, "Payment confirmed");
        
        AddDomainEvent(new OrderConfirmedEvent(this));
    }

    public void StartProcessing()
    {
        if (Status != OrderStatus.Confirmed)
            throw new DomainException($"Cannot process order in {Status} status");

        TransitionTo(OrderStatus.Processing, "Order is being prepared");
    }

    public void Ship(string trackingNumber)
    {
        if (Status != OrderStatus.Processing)
            throw new DomainException($"Cannot ship order in {Status} status");

        if (string.IsNullOrWhiteSpace(trackingNumber))
            throw new DomainException("Tracking number is required");

        TrackingNumber = trackingNumber;
        ShippedAt = DateTime.UtcNow;
        TransitionTo(OrderStatus.Shipped, $"Shipped with tracking: {trackingNumber}");
        
        AddDomainEvent(new OrderShippedEvent(this));
    }

    public void MarkDelivered()
    {
        if (Status != OrderStatus.Shipped)
            throw new DomainException($"Cannot mark delivered from {Status} status");

        DeliveredAt = DateTime.UtcNow;
        TransitionTo(OrderStatus.Delivered, "Package delivered");
        
        AddDomainEvent(new OrderDeliveredEvent(this));
    }

    public void Cancel(string reason)
    {
        if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
            throw new DomainException("Cannot cancel shipped or delivered orders");

        if (Status == OrderStatus.Cancelled)
            throw new DomainException("Order is already cancelled");

        TransitionTo(OrderStatus.Cancelled, $"Cancelled: {reason}");
        
        AddDomainEvent(new OrderCancelledEvent(this, reason));
    }

    public void RequestRefund(string reason)
    {
        if (Status != OrderStatus.Delivered)
            throw new DomainException("Can only request refund for delivered orders");

        TransitionTo(OrderStatus.RefundRequested, $"Refund requested: {reason}");
        
        AddDomainEvent(new RefundRequestedEvent(this, reason));
    }

    private void TransitionTo(OrderStatus newStatus, string notes)
    {
        var previousStatus = Status;
        Status = newStatus;
        
        _statusHistory.Add(new OrderStatusHistory(
            newStatus,
            DateTime.UtcNow,
            notes));
    }

    private Money CalculateSubtotal()
    {
        return _items
            .Select(i => i.LineTotal)
            .Aggregate((a, b) => a.Add(b));
    }

    private Money CalculateShipping()
    {
        // Simplified: free shipping over $50
        return Subtotal.Amount >= 50 
            ? Money.Zero("USD") 
            : Money.FromDecimal(5.99m, "USD");
    }

    private Money CalculateTax()
    {
        // Simplified: 8% tax rate
        return Subtotal.Multiply(0.08m);
    }

    private static string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
    }
}

// Domain/Entities/OrderItem.cs

namespace ShopFlow.Domain.Entities;

public class OrderItem : Entity<int>
{
    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public string ProductName { get; private set; } = null!;
    public string? ProductSku { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = null!;
    public Money LineTotal { get; private set; } = null!;

    private OrderItem() { }

    internal static OrderItem CreateFromCartItem(CartItem cartItem)
    {
        return new OrderItem
        {
            ProductId = cartItem.ProductId,
            ProductName = cartItem.Product.Name,
            ProductSku = cartItem.Product.Sku?.Value,
            Quantity = cartItem.Quantity,
            UnitPrice = cartItem.UnitPrice,
            LineTotal = cartItem.GetLineTotal()
        };
    }
}

// Domain/Enums/OrderStatus.cs

namespace ShopFlow.Domain.Enums;

public enum OrderStatus
{
    Pending = 0,
    Confirmed = 1,
    Processing = 2,
    Shipped = 3,
    Delivered = 4,
    Cancelled = 5,
    RefundRequested = 6,
    Refunded = 7
}

// Domain/Entities/OrderStatusHistory.cs

namespace ShopFlow.Domain.Entities;

public class OrderStatusHistory
{
    public int Id { get; private set; }
    public int OrderId { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string Notes { get; private set; } = null!;

    private OrderStatusHistory() { }

    public OrderStatusHistory(OrderStatus status, DateTime timestamp, string notes)
    {
        Status = status;
        Timestamp = timestamp;
        Notes = notes;
    }
}
```
