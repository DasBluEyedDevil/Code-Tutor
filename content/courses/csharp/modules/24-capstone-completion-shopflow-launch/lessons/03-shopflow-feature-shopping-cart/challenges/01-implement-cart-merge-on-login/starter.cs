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
        // TODO: Implement cart merge logic
        // 1. Get or create authenticated cart for UserId
        // 2. Get anonymous cart by SessionId (may not exist)
        // 3. If anonymous cart exists and has items:
        //    - Call authenticatedCart.MergeFrom(anonymousCart)
        //    - Delete the anonymous cart
        //    - Save changes
        // 4. Map and return the authenticated cart as CartDto
        
        throw new NotImplementedException();
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