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
        // TODO: Implement status transition logic
        // 1. Get the order
        // 2. Check permissions (admin can do anything, user can only cancel their own orders)
        // 3. Apply the appropriate transition based on NewStatus
        // 4. Save changes
        // 5. Publish domain events
        // 6. Return result
        
        throw new NotImplementedException();
    }
}