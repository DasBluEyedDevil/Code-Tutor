using ShopFlow.Application.Carts.DTOs;
using ShopFlow.Application.Carts.Queries;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Interfaces;

namespace ShopFlow.Application.Carts.Handlers;

/// <summary>
/// Handles cart-related queries.
/// </summary>
public class CartQueryHandler
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartQueryHandler(
        ICartRepository cartRepository,
        IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    /// <summary>
    /// Gets a user's cart with product details.
    /// Returns an empty cart representation if the user has no cart.
    /// </summary>
    public async Task<CartDto> HandleAsync(GetCartQuery query, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetByUserIdAsync(query.UserId, cancellationToken);

        if (cart is null)
        {
            // Return an empty cart representation
            return new CartDto(
                0,
                query.UserId,
                Array.Empty<CartItemDto>(),
                0m,
                "USD",
                0,
                DateTime.UtcNow,
                null
            );
        }

        return await MapToDtoWithProductNames(cart, cancellationToken);
    }

    private async Task<CartDto> MapToDtoWithProductNames(Cart cart, CancellationToken cancellationToken)
    {
        var items = new List<CartItemDto>();

        foreach (var item in cart.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);

            items.Add(new CartItemDto(
                item.Id,
                item.ProductId,
                product?.Name,
                item.Quantity,
                item.UnitPrice.Amount,
                item.UnitPrice.Currency,
                item.Subtotal.Amount,
                item.AddedAt
            ));
        }

        var currency = cart.Items.Any() ? cart.Items.First().UnitPrice.Currency : "USD";

        return new CartDto(
            cart.Id,
            cart.UserId,
            items,
            cart.TotalAmount.Amount,
            currency,
            cart.ItemCount,
            cart.CreatedAt,
            cart.UpdatedAt
        );
    }
}
