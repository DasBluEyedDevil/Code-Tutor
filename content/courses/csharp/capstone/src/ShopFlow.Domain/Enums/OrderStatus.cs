namespace ShopFlow.Domain.Enums;

/// <summary>
/// Represents the possible states of an order in its lifecycle.
/// Follows a workflow: Pending -> Confirmed -> Shipped -> Delivered
/// Cancellation is only allowed before Shipped status.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Order has been created but not yet confirmed.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Order has been confirmed and is being processed.
    /// </summary>
    Confirmed = 1,

    /// <summary>
    /// Order has been shipped and is in transit.
    /// </summary>
    Shipped = 2,

    /// <summary>
    /// Order has been delivered to the customer.
    /// </summary>
    Delivered = 3,

    /// <summary>
    /// Order has been cancelled.
    /// </summary>
    Cancelled = 4
}
