using ShopFlow.Application.Orders.Commands;
using ShopFlow.Application.Orders.DTOs;
using ShopFlow.Application.Orders.Handlers;
using ShopFlow.Application.Orders.Queries;
using ShopFlow.Domain.Enums;
using ShopFlow.Domain.Exceptions;

namespace ShopFlow.Api.Endpoints;

/// <summary>
/// Order API endpoints using minimal API pattern.
/// Requires authentication for all endpoints.
/// </summary>
public static class OrderEndpoints
{
    /// <summary>
    /// Maps all order-related endpoints.
    /// </summary>
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders")
            .WithTags("Orders")
            .WithOpenApi()
            .RequireAuthorization();

        group.MapPost("/", CreateOrder)
            .WithName("CreateOrder")
            .WithDescription("Creates an order from the user's cart");

        group.MapGet("/{id:int}", GetOrder)
            .WithName("GetOrder")
            .WithDescription("Gets an order by its ID");

        group.MapPut("/{id:int}/status", UpdateOrderStatus)
            .WithName("UpdateOrderStatus")
            .WithDescription("Updates the status of an order");

        group.MapPost("/{id:int}/cancel", CancelOrder)
            .WithName("CancelOrder")
            .WithDescription("Cancels an order");

        // User-specific endpoint
        var userGroup = app.MapGroup("/api/users")
            .WithTags("Orders")
            .WithOpenApi()
            .RequireAuthorization();

        userGroup.MapGet("/{userId}/orders", GetUserOrders)
            .WithName("GetUserOrders")
            .WithDescription("Gets all orders for a specific user");
    }

    /// <summary>
    /// Creates an order from the user's cart.
    /// </summary>
    private static async Task<IResult> CreateOrder(
        CreateOrderRequest request,
        OrderCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new CreateOrderCommand(request.UserId, request.ShippingAddress);
            var order = await commandHandler.HandleAsync(command, cancellationToken);
            return Results.Created($"/api/orders/{order.Id}", order);
        }
        catch (EntityNotFoundException ex)
        {
            return Results.NotFound(new { Message = ex.Message });
        }
        catch (DomainException ex)
        {
            return Results.BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Gets an order by its ID.
    /// </summary>
    private static async Task<IResult> GetOrder(
        int id,
        OrderQueryHandler queryHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetOrderQuery(id);
            var order = await queryHandler.HandleAsync(query, cancellationToken);
            return Results.Ok(order);
        }
        catch (EntityNotFoundException ex)
        {
            return Results.NotFound(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Gets all orders for a specific user.
    /// </summary>
    private static async Task<IResult> GetUserOrders(
        string userId,
        OrderQueryHandler queryHandler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetUserOrdersQuery(userId);
        var orders = await queryHandler.HandleAsync(query, cancellationToken);
        return Results.Ok(orders);
    }

    /// <summary>
    /// Updates the status of an order.
    /// </summary>
    private static async Task<IResult> UpdateOrderStatus(
        int id,
        UpdateOrderStatusRequest request,
        OrderCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new UpdateOrderStatusCommand(id, request.Status);
            var order = await commandHandler.HandleAsync(command, cancellationToken);
            return Results.Ok(order);
        }
        catch (EntityNotFoundException ex)
        {
            return Results.NotFound(new { Message = ex.Message });
        }
        catch (DomainException ex)
        {
            return Results.BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Cancels an order.
    /// </summary>
    private static async Task<IResult> CancelOrder(
        int id,
        OrderCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new CancelOrderCommand(id);
            var order = await commandHandler.HandleAsync(command, cancellationToken);
            return Results.Ok(order);
        }
        catch (EntityNotFoundException ex)
        {
            return Results.NotFound(new { Message = ex.Message });
        }
        catch (DomainException ex)
        {
            return Results.BadRequest(new { Message = ex.Message });
        }
    }
}

/// <summary>
/// Request model for creating an order.
/// </summary>
public record CreateOrderRequest(
    string UserId,
    string? ShippingAddress = null
);

/// <summary>
/// Request model for updating order status.
/// </summary>
public record UpdateOrderStatusRequest(
    OrderStatus Status
);
