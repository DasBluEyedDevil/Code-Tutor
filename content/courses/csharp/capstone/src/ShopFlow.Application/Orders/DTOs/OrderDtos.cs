using ShopFlow.Domain.Enums;

namespace ShopFlow.Application.Orders.DTOs;

/// <summary>
/// Data Transfer Object for Order entity with full details.
/// </summary>
public record OrderDto(
    int Id,
    string UserId,
    OrderStatus Status,
    string StatusName,
    IReadOnlyList<OrderItemDto> Items,
    decimal TotalAmount,
    string Currency,
    int ItemCount,
    string? ShippingAddress,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? ConfirmedAt,
    DateTime? ShippedAt,
    DateTime? DeliveredAt,
    DateTime? CancelledAt,
    bool CanBeCancelled,
    bool CanBeModified
);

/// <summary>
/// Data Transfer Object for OrderItem entity.
/// </summary>
public record OrderItemDto(
    int Id,
    int ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    string Currency,
    decimal Subtotal
);

/// <summary>
/// Simplified Order DTO for list views (order history).
/// </summary>
public record OrderSummaryDto(
    int Id,
    OrderStatus Status,
    string StatusName,
    decimal TotalAmount,
    string Currency,
    int ItemCount,
    DateTime CreatedAt
);
