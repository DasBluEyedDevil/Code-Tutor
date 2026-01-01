namespace ShopFlow.Application.Carts.DTOs;

/// <summary>
/// Data Transfer Object for Cart entity.
/// </summary>
public record CartDto(
    int Id,
    string UserId,
    IReadOnlyList<CartItemDto> Items,
    decimal TotalAmount,
    string Currency,
    int ItemCount,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

/// <summary>
/// Data Transfer Object for CartItem entity.
/// </summary>
public record CartItemDto(
    int Id,
    int ProductId,
    string? ProductName,
    int Quantity,
    decimal UnitPrice,
    string Currency,
    decimal Subtotal,
    DateTime AddedAt
);
