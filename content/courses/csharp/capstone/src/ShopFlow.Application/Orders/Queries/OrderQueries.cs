namespace ShopFlow.Application.Orders.Queries;

/// <summary>
/// Query to get an order by its ID.
/// </summary>
public record GetOrderQuery(int OrderId);

/// <summary>
/// Query to get all orders for a specific user.
/// </summary>
public record GetUserOrdersQuery(string UserId);
