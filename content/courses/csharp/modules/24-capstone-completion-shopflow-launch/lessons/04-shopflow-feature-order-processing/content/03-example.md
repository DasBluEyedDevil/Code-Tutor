---
type: "EXAMPLE"
title: "Order Processing Workflow"
---

The PlaceOrder command orchestrates the complete order creation workflow: validating the cart, checking inventory, processing payment, creating the order, and publishing events.

```csharp
// Application/Orders/Commands/PlaceOrderCommand.cs

using MediatR;
using ShopFlow.Application.Common;
using ShopFlow.Application.Orders.Dtos;

namespace ShopFlow.Application.Orders.Commands;

public record PlaceOrderCommand : IRequest<Result<OrderDto>>
{
    public required int UserId { get; init; }
    public required AddressDto ShippingAddress { get; init; }
    public AddressDto? BillingAddress { get; init; }
    public required string PaymentMethodId { get; init; }
}

// Application/Orders/Handlers/PlaceOrderHandler.cs

using MediatR;
using Microsoft.Extensions.Logging;
using ShopFlow.Application.Common;
using ShopFlow.Application.Orders.Commands;
using ShopFlow.Application.Orders.Dtos;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Repositories;
using ShopFlow.Domain.Services;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Application.Orders.Handlers;

public class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand, Result<OrderDto>>
{
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryService _inventoryService;
    private readonly IPaymentService _paymentService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<PlaceOrderHandler> _logger;

    public PlaceOrderHandler(
        ICartRepository cartRepository,
        IOrderRepository orderRepository,
        IInventoryService inventoryService,
        IPaymentService paymentService,
        IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher,
        ILogger<PlaceOrderHandler> logger)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
        _inventoryService = inventoryService;
        _paymentService = paymentService;
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task<Result<OrderDto>> Handle(
        PlaceOrderCommand command, 
        CancellationToken ct)
    {
        _logger.LogInformation("Processing order for user {UserId}", command.UserId);

        // 1. Get and validate cart
        var cart = await _cartRepository.GetByUserIdAsync(command.UserId, ct);
        
        if (cart is null || !cart.Items.Any())
        {
            _logger.LogWarning("Order failed: Empty cart for user {UserId}", command.UserId);
            return Result<OrderDto>.Failure("Cart is empty");
        }

        // 2. Check inventory for all items
        var inventoryResult = await _inventoryService.CheckAndReserveAsync(
            cart.Items.Select(i => new InventoryRequest(i.ProductId, i.Quantity)),
            ct);

        if (!inventoryResult.Success)
        {
            _logger.LogWarning(
                "Order failed: Insufficient inventory. {Details}", 
                inventoryResult.Message);
            return Result<OrderDto>.Failure($"Inventory unavailable: {inventoryResult.Message}");
        }

        try
        {
            // 3. Create the order
            var shippingAddress = MapToAddress(command.ShippingAddress);
            var billingAddress = command.BillingAddress is not null 
                ? MapToAddress(command.BillingAddress) 
                : null;

            var order = Order.CreateFromCart(
                cart, 
                command.UserId, 
                shippingAddress, 
                billingAddress);

            // 4. Process payment
            var paymentResult = await _paymentService.ProcessPaymentAsync(
                command.PaymentMethodId,
                order.Total,
                new PaymentMetadata
                {
                    OrderNumber = order.OrderNumber,
                    CustomerId = command.UserId.ToString()
                },
                ct);

            if (!paymentResult.Success)
            {
                // Release inventory reservation on payment failure
                await _inventoryService.ReleaseReservationAsync(inventoryResult.ReservationId, ct);
                
                _logger.LogWarning(
                    "Order failed: Payment declined. {Message}", 
                    paymentResult.ErrorMessage);
                return Result<OrderDto>.Failure($"Payment failed: {paymentResult.ErrorMessage}");
            }

            // 5. Confirm the order with payment intent
            order.Confirm(paymentResult.PaymentIntentId);

            // 6. Save order and clear cart
            await _orderRepository.AddAsync(order, ct);
            cart.Clear();
            
            await _unitOfWork.SaveChangesAsync(ct);

            // 7. Commit inventory reservation
            await _inventoryService.CommitReservationAsync(inventoryResult.ReservationId, ct);

            // 8. Publish domain events
            foreach (var domainEvent in order.DomainEvents)
            {
                await _eventPublisher.PublishAsync(domainEvent, ct);
            }
            order.ClearDomainEvents();

            _logger.LogInformation(
                "Order {OrderNumber} placed successfully for user {UserId}", 
                order.OrderNumber, 
                command.UserId);

            return Result<OrderDto>.Success(MapToDto(order));
        }
        catch (Exception ex)
        {
            // Release inventory on any failure
            await _inventoryService.ReleaseReservationAsync(inventoryResult.ReservationId, ct);
            
            _logger.LogError(ex, "Order placement failed unexpectedly for user {UserId}", command.UserId);
            throw;
        }
    }

    private static Address MapToAddress(AddressDto dto)
    {
        return new Address(
            dto.Street,
            dto.City,
            dto.State,
            dto.PostalCode,
            dto.Country);
    }

    private static OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            Status = order.Status.ToString(),
            Items = order.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice.Amount,
                LineTotal = i.LineTotal.Amount
            }).ToList(),
            Subtotal = order.Subtotal.Amount,
            ShippingCost = order.ShippingCost.Amount,
            Tax = order.Tax.Amount,
            Total = order.Total.Amount,
            ShippingAddress = MapAddressToDto(order.ShippingAddress),
            CreatedAt = order.CreatedAt
        };
    }

    private static AddressDto MapAddressToDto(Address address)
    {
        return new AddressDto
        {
            Street = address.Street,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country
        };
    }
}
```
