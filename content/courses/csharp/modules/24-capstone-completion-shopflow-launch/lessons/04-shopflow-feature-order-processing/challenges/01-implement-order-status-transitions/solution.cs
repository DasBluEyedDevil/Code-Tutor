// Application/Orders/Commands/UpdateOrderStatusCommand.cs
using MediatR;
using ShopFlow.Application.Common;
using ShopFlow.Domain.Enums;

namespace ShopFlow.Application.Orders.Commands;

public record UpdateOrderStatusCommand : IRequest<Result<OrderStatusDto>>
{
    public required int OrderId { get; init; }
    public required OrderStatus NewStatus { get; init; }
    public string? TrackingNumber { get; init; }
    public string? Reason { get; init; }
    public required int RequestedByUserId { get; init; }
    public required bool IsAdmin { get; init; }
}

// Application/Orders/Handlers/UpdateOrderStatusHandler.cs
using MediatR;
using ShopFlow.Application.Common;
using ShopFlow.Application.Orders.Commands;
using ShopFlow.Domain.Enums;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Repositories;

namespace ShopFlow.Application.Orders.Handlers;

public class UpdateOrderStatusHandler 
    : IRequestHandler<UpdateOrderStatusCommand, Result<OrderStatusDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public UpdateOrderStatusHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }

    public async Task<Result<OrderStatusDto>> Handle(
        UpdateOrderStatusCommand command, 
        CancellationToken ct)
    {
        // 1. Get the order
        var order = await _orderRepository.GetByIdAsync(command.OrderId, ct);
        
        if (order is null)
        {
            return Result<OrderStatusDto>.Failure($"Order {command.OrderId} not found");
        }

        // 2. Check permissions
        var isOwner = order.UserId == command.RequestedByUserId;
        var canCancel = command.IsAdmin || (isOwner && command.NewStatus == OrderStatus.Cancelled);
        var canUpdateOtherStatus = command.IsAdmin;

        if (command.NewStatus == OrderStatus.Cancelled && !canCancel)
        {
            return Result<OrderStatusDto>.Failure("You can only cancel your own orders");
        }

        if (command.NewStatus != OrderStatus.Cancelled && !canUpdateOtherStatus)
        {
            return Result<OrderStatusDto>.Failure("Only administrators can update order status");
        }

        // 3. Apply the appropriate transition
        try
        {
            switch (command.NewStatus)
            {
                case OrderStatus.Processing:
                    order.StartProcessing();
                    break;

                case OrderStatus.Shipped:
                    if (string.IsNullOrWhiteSpace(command.TrackingNumber))
                    {
                        return Result<OrderStatusDto>.Failure("Tracking number is required for shipping");
                    }
                    order.Ship(command.TrackingNumber);
                    break;

                case OrderStatus.Delivered:
                    order.MarkDelivered();
                    break;

                case OrderStatus.Cancelled:
                    if (string.IsNullOrWhiteSpace(command.Reason))
                    {
                        return Result<OrderStatusDto>.Failure("Reason is required for cancellation");
                    }
                    order.Cancel(command.Reason);
                    break;

                default:
                    return Result<OrderStatusDto>.Failure($"Cannot transition to {command.NewStatus}");
            }
        }
        catch (DomainException ex)
        {
            return Result<OrderStatusDto>.Failure(ex.Message);
        }

        // 4. Save changes
        await _unitOfWork.SaveChangesAsync(ct);

        // 5. Publish domain events
        foreach (var domainEvent in order.DomainEvents)
        {
            await _eventPublisher.PublishAsync(domainEvent, ct);
        }
        order.ClearDomainEvents();

        // 6. Return result
        return Result<OrderStatusDto>.Success(new OrderStatusDto
        {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            Status = order.Status.ToString(),
            UpdatedAt = DateTime.UtcNow
        });
    }
}