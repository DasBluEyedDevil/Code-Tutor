using ShopFlow.Application.Carts.Commands;
using ShopFlow.Application.Carts.DTOs;
using ShopFlow.Application.Common.Interfaces;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;

namespace ShopFlow.Application.Carts.Handlers;

/// <summary>
/// Handles cart-related commands.
/// </summary>
public class CartCommandHandler
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CartCommandHandler(
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Adds an item to a user's cart.
    /// Creates the cart if it doesn't exist.
    /// </summary>
    public async Task<CartDto> HandleAsync(AddToCartCommand command, CancellationToken cancellationToken = default)
    {
        // Validate product exists and has sufficient stock
        var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken)
            ?? throw new EntityNotFoundException("Product", command.ProductId);

        if (!product.HasSufficientStock(command.Quantity))
        {
            throw new DomainException($"Insufficient stock for product '{product.Name}'. Available: {product.StockQuantity}, Requested: {command.Quantity}");
        }

        // Get or create cart
        var cart = await _cartRepository.GetByUserIdAsync(command.UserId, cancellationToken);
        var isNewCart = cart is null;

        if (isNewCart)
        {
            cart = Cart.Create(command.UserId);
        }

        // Check total quantity if adding to existing item
        var existingItem = cart!.GetItem(command.ProductId);
        var totalQuantity = (existingItem?.Quantity ?? 0) + command.Quantity;
        if (!product.HasSufficientStock(totalQuantity))
        {
            throw new DomainException($"Insufficient stock for product '{product.Name}'. Available: {product.StockQuantity}, Total requested: {totalQuantity}");
        }

        cart.AddItem(command.ProductId, command.Quantity, product.Price);

        if (isNewCart)
        {
            await _cartRepository.AddAsync(cart, cancellationToken);
        }
        else
        {
            _cartRepository.Update(cart);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(cart, product);
    }

    /// <summary>
    /// Removes an item from a user's cart.
    /// </summary>
    public async Task<CartDto> HandleAsync(RemoveFromCartCommand command, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetByUserIdAsync(command.UserId, cancellationToken)
            ?? throw new EntityNotFoundException("Cart", command.UserId);

        cart.RemoveItem(command.ProductId);

        _cartRepository.Update(cart);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(cart);
    }

    /// <summary>
    /// Updates the quantity of an item in a user's cart.
    /// </summary>
    public async Task<CartDto> HandleAsync(UpdateCartItemQuantityCommand command, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetByUserIdAsync(command.UserId, cancellationToken)
            ?? throw new EntityNotFoundException("Cart", command.UserId);

        // If quantity > 0, validate stock
        if (command.Quantity > 0)
        {
            var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken)
                ?? throw new EntityNotFoundException("Product", command.ProductId);

            if (!product.HasSufficientStock(command.Quantity))
            {
                throw new DomainException($"Insufficient stock for product '{product.Name}'. Available: {product.StockQuantity}, Requested: {command.Quantity}");
            }
        }

        cart.UpdateItemQuantity(command.ProductId, command.Quantity);

        _cartRepository.Update(cart);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(cart);
    }

    /// <summary>
    /// Clears all items from a user's cart.
    /// </summary>
    public async Task HandleAsync(ClearCartCommand command, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetByUserIdAsync(command.UserId, cancellationToken)
            ?? throw new EntityNotFoundException("Cart", command.UserId);

        cart.Clear();

        _cartRepository.Update(cart);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static CartDto MapToDto(Cart cart, Product? singleProduct = null)
    {
        var items = cart.Items.Select(item => new CartItemDto(
            item.Id,
            item.ProductId,
            singleProduct?.Id == item.ProductId ? singleProduct.Name : null,
            item.Quantity,
            item.UnitPrice.Amount,
            item.UnitPrice.Currency,
            item.Subtotal.Amount,
            item.AddedAt
        )).ToList();

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
