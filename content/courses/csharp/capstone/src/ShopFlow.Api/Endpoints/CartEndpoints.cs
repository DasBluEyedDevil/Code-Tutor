using ShopFlow.Application.Carts.Commands;
using ShopFlow.Application.Carts.DTOs;
using ShopFlow.Application.Carts.Handlers;
using ShopFlow.Application.Carts.Queries;
using ShopFlow.Domain.Exceptions;

namespace ShopFlow.Api.Endpoints;

/// <summary>
/// Cart API endpoints using minimal API pattern.
/// Requires authentication for all endpoints.
/// </summary>
public static class CartEndpoints
{
    /// <summary>
    /// Maps all cart-related endpoints.
    /// </summary>
    public static void MapCartEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/carts")
            .WithTags("Carts")
            .WithOpenApi()
            .RequireAuthorization();

        group.MapGet("/{userId}", GetCart)
            .WithName("GetCart")
            .WithDescription("Gets a user's shopping cart");

        group.MapPost("/{userId}/items", AddToCart)
            .WithName("AddToCart")
            .WithDescription("Adds an item to the user's cart");

        group.MapPut("/{userId}/items/{productId:int}", UpdateCartItemQuantity)
            .WithName("UpdateCartItemQuantity")
            .WithDescription("Updates the quantity of an item in the cart");

        group.MapDelete("/{userId}/items/{productId:int}", RemoveFromCart)
            .WithName("RemoveFromCart")
            .WithDescription("Removes an item from the cart");

        group.MapDelete("/{userId}", ClearCart)
            .WithName("ClearCart")
            .WithDescription("Clears all items from the cart");
    }

    /// <summary>
    /// Gets a user's shopping cart.
    /// </summary>
    private static async Task<IResult> GetCart(
        string userId,
        CartQueryHandler queryHandler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCartQuery(userId);
        var cart = await queryHandler.HandleAsync(query, cancellationToken);
        return Results.Ok(cart);
    }

    /// <summary>
    /// Adds an item to the user's cart.
    /// </summary>
    private static async Task<IResult> AddToCart(
        string userId,
        AddToCartRequest request,
        CartCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new AddToCartCommand(userId, request.ProductId, request.Quantity);
            var cart = await commandHandler.HandleAsync(command, cancellationToken);
            return Results.Ok(cart);
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
    /// Updates the quantity of an item in the cart.
    /// </summary>
    private static async Task<IResult> UpdateCartItemQuantity(
        string userId,
        int productId,
        UpdateCartItemRequest request,
        CartCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new UpdateCartItemQuantityCommand(userId, productId, request.Quantity);
            var cart = await commandHandler.HandleAsync(command, cancellationToken);
            return Results.Ok(cart);
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
    /// Removes an item from the cart.
    /// </summary>
    private static async Task<IResult> RemoveFromCart(
        string userId,
        int productId,
        CartCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new RemoveFromCartCommand(userId, productId);
            var cart = await commandHandler.HandleAsync(command, cancellationToken);
            return Results.Ok(cart);
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
    /// Clears all items from the cart.
    /// </summary>
    private static async Task<IResult> ClearCart(
        string userId,
        CartCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new ClearCartCommand(userId);
            await commandHandler.HandleAsync(command, cancellationToken);
            return Results.NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            return Results.NotFound(new { Message = ex.Message });
        }
    }
}

/// <summary>
/// Request model for adding an item to the cart.
/// </summary>
public record AddToCartRequest(
    int ProductId,
    int Quantity
);

/// <summary>
/// Request model for updating an item's quantity.
/// </summary>
public record UpdateCartItemRequest(
    int Quantity
);
