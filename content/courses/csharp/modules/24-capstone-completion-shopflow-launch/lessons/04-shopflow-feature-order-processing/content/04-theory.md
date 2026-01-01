---
type: "THEORY"
title: "Event-Driven Order Processing"
---

As orders move through their lifecycle, multiple systems need to react: inventory must be updated, customers must be notified, analytics must be recorded, and fulfillment must be triggered. Event-driven architecture decouples these concerns, making the system more maintainable and scalable.

## Domain Events

Domain events capture significant state changes within the domain model. When an order is placed, shipped, or cancelled, the Order entity raises events that other parts of the system can handle independently.

```csharp
public record OrderCreatedEvent(Order Order) : IDomainEvent;
public record OrderConfirmedEvent(Order Order) : IDomainEvent;
public record OrderShippedEvent(Order Order) : IDomainEvent;
public record OrderDeliveredEvent(Order Order) : IDomainEvent;
public record OrderCancelledEvent(Order Order, string Reason) : IDomainEvent;

// Event handlers react to domain events
public class SendOrderConfirmationEmailHandler 
    : INotificationHandler<OrderConfirmedEvent>
{
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;

    public async Task Handle(OrderConfirmedEvent notification, CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(notification.Order.UserId, ct);
        
        await _emailService.SendAsync(new OrderConfirmationEmail
        {
            To = user.Email,
            OrderNumber = notification.Order.OrderNumber,
            Total = notification.Order.Total,
            Items = notification.Order.Items
        }, ct);
    }
}
```

## Eventual Consistency

In a distributed system, not all operations can complete atomically. When an order is placed, the order record must be saved immediately, but sending confirmation emails, updating analytics, and notifying fulfillment centers can happen asynchronously. This eventual consistency model improves response times and system resilience.

```csharp
// Outbox pattern ensures events are eventually published
public class OutboxEventPublisher : IEventPublisher
{
    private readonly ShopFlowDbContext _context;

    public async Task PublishAsync(IDomainEvent domainEvent, CancellationToken ct)
    {
        // Save event to outbox table within the same transaction
        var outboxMessage = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = domainEvent.GetType().AssemblyQualifiedName,
            Payload = JsonSerializer.Serialize(domainEvent, domainEvent.GetType()),
            CreatedAt = DateTime.UtcNow,
            ProcessedAt = null
        };

        _context.OutboxMessages.Add(outboxMessage);
        // SaveChanges happens with the main transaction
    }
}

// Background job processes outbox messages
public class OutboxProcessor : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            var messages = await _context.OutboxMessages
                .Where(m => m.ProcessedAt == null)
                .OrderBy(m => m.CreatedAt)
                .Take(100)
                .ToListAsync(ct);

            foreach (var message in messages)
            {
                await PublishToMessageBus(message, ct);
                message.ProcessedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(ct);
            await Task.Delay(TimeSpan.FromSeconds(5), ct);
        }
    }
}
```

## Saga Pattern Introduction

Complex order workflows span multiple services and can fail at any step. The Saga pattern coordinates these distributed transactions by defining a sequence of local transactions with compensating actions for rollback.

```csharp
// Simplified saga coordinator for order fulfillment
public class OrderFulfillmentSaga
{
    public async Task ExecuteAsync(Order order, CancellationToken ct)
    {
        var sagaState = new SagaState();

        try
        {
            // Step 1: Reserve inventory
            sagaState.InventoryReservationId = await _inventory.ReserveAsync(order.Items, ct);

            // Step 2: Charge payment
            sagaState.PaymentTransactionId = await _payment.ChargeAsync(order.Total, ct);

            // Step 3: Schedule shipment
            sagaState.ShipmentId = await _shipping.ScheduleAsync(order, ct);

            // All steps succeeded
            await _orders.MarkFulfilledAsync(order.Id, ct);
        }
        catch (Exception ex)
        {
            // Compensate in reverse order
            if (sagaState.ShipmentId is not null)
                await _shipping.CancelAsync(sagaState.ShipmentId, ct);

            if (sagaState.PaymentTransactionId is not null)
                await _payment.RefundAsync(sagaState.PaymentTransactionId, ct);

            if (sagaState.InventoryReservationId is not null)
                await _inventory.ReleaseAsync(sagaState.InventoryReservationId, ct);

            await _orders.MarkFailedAsync(order.Id, ex.Message, ct);
        }
    }
}
```

The saga pattern ensures that even when individual services fail, the overall system remains consistent through compensating transactions. This is essential for reliable order processing in distributed e-commerce systems.