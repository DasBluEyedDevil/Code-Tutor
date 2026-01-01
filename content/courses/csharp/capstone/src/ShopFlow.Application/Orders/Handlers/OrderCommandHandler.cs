using ShopFlow.Application.Common.Interfaces;
using ShopFlow.Application.Orders.Commands;
using ShopFlow.Application.Orders.DTOs;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;

namespace ShopFlow.Application.Orders.Handlers;

/// <summary>
/// Handles order-related commands.
/// </summary>
public class OrderCommandHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderCommandHandler(
        IOrderRepository orderRepository,
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates an order from a user's cart.
    /// Reduces product stock and clears the cart after creation.
    /// </summary>
    public async Task<OrderDto> HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        // Get the user's cart
        var cart = await _cartRepository.GetByUserIdAsync(command.UserId, cancellationToken)
            ?? throw new EntityNotFoundException("Cart", command.UserId);

        if (!cart.Items.Any())
            throw new DomainException("Cannot create an order from an empty cart.");

        // Build product names dictionary and validate stock
        var productNames = new Dictionary<int, string>();
        var productsByCartItemId = new Dictionary<int, Product>();

        foreach (var item in cart.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken)
                ?? throw new EntityNotFoundException("Product", item.ProductId);

            if (!product.HasSufficientStock(item.Quantity))
            {
                throw new DomainException($"Insufficient stock for product '{product.Name}'. Available: {product.StockQuantity}, Requested: {item.Quantity}");
            }

            productNames[item.ProductId] = product.Name;
            productsByCartItemId[item.ProductId] = product;
        }

        // Create the order from cart
        var order = Order.CreateFromCart(cart, productNames, command.ShippingAddress);

        // Reduce stock for each product
        foreach (var item in cart.Items)
        {
            var product = productsByCartItemId[item.ProductId];
            product.RemoveStock(item.Quantity);
        }

        // Add order, clear cart
        await _orderRepository.AddAsync(order, cancellationToken);
        cart.Clear();
        _cartRepository.Update(cart);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(order);
    }

    /// <summary>
    /// Updates the status of an order.
    /// </summary>
    public async Task<OrderDto> HandleAsync(UpdateOrderStatusCommand command, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken)
            ?? throw new EntityNotFoundException("Order", command.OrderId);

        order.UpdateStatus(command.NewStatus);

        _orderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(order);
    }

    /// <summary>
    /// Cancels an order.
    /// </summary>
    public async Task<OrderDto> HandleAsync(CancelOrderCommand command, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken)
            ?? throw new EntityNotFoundException("Order", command.OrderId);

        order.Cancel();

        // Restore stock for each product
        foreach (var item in order.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
            if (product is not null)
            {
                product.AddStock(item.Quantity);
            }
        }

        _orderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(order);
    }

    private static OrderDto MapToDto(Order order)
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
}
