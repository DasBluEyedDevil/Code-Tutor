using ShopFlow.Domain.Enums;

namespace ShopFlow.Application.Orders.Commands;

/// <summary>
/// Command to create an order from a user's cart.
/// </summary>
public record CreateOrderCommand(
    string UserId,
    string? ShippingAddress = null
);

/// <summary>
/// Command to update an order's status.
/// </summary>
public record UpdateOrderStatusCommand(
    int OrderId,
    OrderStatus NewStatus
);

/// <summary>
/// Command to cancel an order.
/// </summary>
public record CancelOrderCommand(int OrderId);
