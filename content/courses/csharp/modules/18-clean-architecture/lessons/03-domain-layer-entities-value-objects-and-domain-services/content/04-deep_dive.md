---
type: "THEORY"
title: "Domain Events"
---

## Domain Events: Communicating What Happened

Domain Events are a powerful pattern for decoupling side effects from domain logic. Instead of an entity directly calling external services, it raises an event that other parts of the system can react to.

**THE PROBLEM WITHOUT DOMAIN EVENTS:**

When an order is placed, you need to:
- Send confirmation email
- Update inventory
- Notify warehouse
- Record analytics

Without Domain Events, you might pollute your Order entity with all these concerns, or create a massive Application Service that does everything.

**THE SOLUTION - DOMAIN EVENTS:**

```csharp
// Domain Event - describes what happened
namespace ShopFlow.Domain.Events;

public record OrderPlacedEvent(
    int OrderId,
    int CustomerId,
    IReadOnlyList<OrderItemInfo> Items,
    Money TotalAmount,
    DateTime OccurredAt
) : IDomainEvent;

public record OrderItemInfo(int ProductId, int Quantity, Money UnitPrice);

// Base interface for all domain events
public interface IDomainEvent
{
    DateTime OccurredAt { get; }
}


// Entity raises events
namespace ShopFlow.Domain.Entities;

public class Order
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void Place()
    {
        if (Status != OrderStatus.Draft)
            throw new DomainException("Can only place draft orders");

        Status = OrderStatus.Placed;
        PlacedAt = DateTime.UtcNow;

        // Raise event - no direct dependencies!
        _domainEvents.Add(new OrderPlacedEvent(
            Id,
            CustomerId,
            Items.Select(i => new OrderItemInfo(i.ProductId, i.Quantity, i.UnitPrice)).ToList(),
            TotalAmount,
            DateTime.UtcNow
        ));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}


// Event Handler in Application Layer
namespace ShopFlow.Application.Orders.EventHandlers;

public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
{
    private readonly IEmailService _emailService;
    private readonly IProductRepository _productRepository;
    private readonly IAnalyticsService _analytics;

    public OrderPlacedEventHandler(
        IEmailService emailService,
        IProductRepository productRepository,
        IAnalyticsService analytics)
    {
        _emailService = emailService;
        _productRepository = productRepository;
        _analytics = analytics;
    }

    public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
    {
        // Update inventory
        foreach (var item in notification.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
            product?.RemoveStock(item.Quantity);
        }

        // Send email (Infrastructure handles the actual sending)
        await _emailService.SendOrderConfirmationAsync(
            notification.OrderId,
            cancellationToken);

        // Record analytics
        await _analytics.TrackOrderPlacedAsync(notification, cancellationToken);
    }
}


// Dispatching events (usually in UnitOfWork or DbContext)
namespace ShopFlow.Infrastructure.Data;

public class AppDbContext : DbContext
{
    private readonly IMediator _mediator;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch domain events before saving
        await DispatchDomainEventsAsync(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
    {
        var entities = ChangeTracker
            .Entries<Entity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var events = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in events)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
```

**BENEFITS OF DOMAIN EVENTS:**

1. **Decoupling**: Order entity doesn't know about emails, analytics, or warehouses
2. **Single Responsibility**: Each handler does one thing
3. **Testability**: Test Order.Place() without mocking email services
4. **Extensibility**: Add new handlers without modifying Order entity
5. **Auditability**: Events are a natural audit log

**WHEN TO USE DOMAIN EVENTS:**

- When something happened that other parts of the system care about
- When you have multiple side effects from one action
- When you want to avoid coupling between aggregates
- When you need an audit trail of what happened