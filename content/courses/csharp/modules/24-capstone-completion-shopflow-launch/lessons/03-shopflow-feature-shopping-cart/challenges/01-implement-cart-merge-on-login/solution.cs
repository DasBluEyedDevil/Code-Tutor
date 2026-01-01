// Application/Cart/Commands/MergeCartCommand.cs
using MediatR;
using ShopFlow.Application.Cart.Dtos;

namespace ShopFlow.Application.Cart.Commands;

public record MergeCartCommand : IRequest<CartDto>
{
    public required int UserId { get; init; }
    public required string SessionId { get; init; }
}

// Application/Cart/Handlers/MergeCartHandler.cs
using MediatR;
using ShopFlow.Application.Cart.Commands;
using ShopFlow.Application.Cart.Dtos;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Repositories;

namespace ShopFlow.Application.Cart.Handlers;

public class MergeCartHandler : IRequestHandler<MergeCartCommand, CartDto>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MergeCartHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CartDto> Handle(MergeCartCommand command, CancellationToken ct)
    {
        // Get or create authenticated cart
        var authenticatedCart = await _cartRepository.GetByUserIdAsync(command.UserId, ct);
        
        if (authenticatedCart is null)
        {
            authenticatedCart = Cart.CreateForUser(command.UserId);
            await _cartRepository.AddAsync(authenticatedCart, ct);
        }

        // Get anonymous cart - may not exist
        var anonymousCart = await _cartRepository.GetBySessionIdAsync(command.SessionId, ct);

        // If anonymous cart exists and has items, merge them
        if (anonymousCart is not null && anonymousCart.Items.Any())
        {
            // Use domain method that handles merge logic
            authenticatedCart.MergeFrom(anonymousCart);

            // Delete the anonymous cart after merge
            await _cartRepository.DeleteAsync(anonymousCart, ct);
        }

        // Save all changes
        await _unitOfWork.SaveChangesAsync(ct);

        return MapToDto(authenticatedCart);
    }

    private CartDto MapToDto(Cart cart)
    {
        return new CartDto
        {
            Id = cart.Id,
            Items = cart.Items.Select(i => new CartItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice.Amount,
                LineTotal = i.GetLineTotal().Amount
            }).ToList(),
            Subtotal = cart.GetSubtotal().Amount,
            ItemCount = cart.Items.Sum(i => i.Quantity)
        };
    }
}