namespace ShopFlow.Application.Carts.Commands;

/// <summary>
/// Command to add an item to a cart.
/// </summary>
public record AddToCartCommand(
    string UserId,
    int ProductId,
    int Quantity
);

/// <summary>
/// Command to remove an item from a cart.
/// </summary>
public record RemoveFromCartCommand(
    string UserId,
    int ProductId
);

/// <summary>
/// Command to update an item's quantity in a cart.
/// </summary>
public record UpdateCartItemQuantityCommand(
    string UserId,
    int ProductId,
    int Quantity
);

/// <summary>
/// Command to clear all items from a cart.
/// </summary>
public record ClearCartCommand(string UserId);
