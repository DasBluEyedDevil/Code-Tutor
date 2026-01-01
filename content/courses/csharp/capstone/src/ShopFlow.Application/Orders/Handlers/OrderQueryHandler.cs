using ShopFlow.Application.Orders.DTOs;
using ShopFlow.Application.Orders.Queries;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;

namespace ShopFlow.Application.Orders.Handlers;

/// <summary>
/// Handles order-related queries.
/// </summary>
public class OrderQueryHandler
{
    private readonly IOrderRepository _orderRepository;

    public OrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Gets an order by its ID.
    /// </summary>
    public async Task<OrderDto> HandleAsync(GetOrderQuery query, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(query.OrderId, cancellationToken)
            ?? throw new EntityNotFoundException("Order", query.OrderId);

        return MapToDto(order);
    }

    /// <summary>
    /// Gets all orders for a specific user.
    /// </summary>
    public async Task<IReadOnlyList<OrderSummaryDto>> HandleAsync(GetUserOrdersQuery query, CancellationToken cancellationToken = default)
    {
        var orders = await _orderRepository.GetByUserIdAsync(query.UserId, cancellationToken);

        return orders.Select(MapToSummaryDto).ToList();
    }

    private static OrderDto MapToDto(Domain.Entities.Order order)
    {
        var items = order.Items.Select(item => new OrderItemDto(
            item.Id,
            item.ProductId,
            item.ProductName,
            item.Quantity,
            item.UnitPrice.Amount,
            item.UnitPrice.Currency,
            item.Subtotal.Amount
        )).ToList();

        var currency = order.Items.Any() ? order.Items.First().UnitPrice.Currency : "USD";

        return new OrderDto(
            order.Id,
            order.UserId,
            order.Status,
            order.Status.ToString(),
            items,
            order.TotalAmount.Amount,
            currency,
            order.ItemCount,
            order.ShippingAddress,
            order.CreatedAt,
            order.UpdatedAt,
            order.ConfirmedAt,
            order.ShippedAt,
            order.DeliveredAt,
            order.CancelledAt,
            order.CanBeCancelled,
            order.CanBeModified
        );
    }

    private static OrderSummaryDto MapToSummaryDto(Domain.Entities.Order order)
    {
        var currency = order.Items.Any() ? order.Items.First().UnitPrice.Currency : "USD";

        return new OrderSummaryDto(
            order.Id,
            order.Status,
            order.Status.ToString(),
            order.TotalAmount.Amount,
            currency,
            order.ItemCount,
            order.CreatedAt
        );
    }
}
