---
type: "THEORY"
title: "When Resource-Based Authorization is Required"
---

## Ownership-Based Access

The most common scenario for resource-based authorization is ownership. Users should only access resources they own. In ShopFlow:

- **Customers** can view, cancel, and request refunds for their own orders
- **Sellers** can edit and delete their own products
- **Users** can update their own profile information

Role-based authorization cannot express 'your own' - it only knows 'user is a Customer'. Resource-based authorization adds the critical context: 'user is the Customer who placed THIS order'.

## Per-Resource Permissions

Some systems grant permissions on specific resources rather than globally. A document collaboration platform might allow:

- User A has 'edit' permission on Document 1
- User A has 'view-only' permission on Document 2
- User A has no access to Document 3

The same user has different permissions on different resources of the same type. This requires examining the resource's permission list, not just the user's roles or claims.

## Business Rule Enforcement

Authorization often depends on business state, not just identity. Can a user cancel this order? It depends on:

- Is this their order? (ownership)
- What is the order status? (business state)
- How long ago was the order placed? (time-based rule)
- Has the order shipped? (operational state)

These rules require examining the order entity. A policy cannot answer these questions without the resource.

## Hierarchical Resources

When resources have parent-child relationships, access decisions cascade. Can a user view Comment #789? That depends on:

- Can they view the Post containing the comment?
- Is the comment from a user they follow?
- Is the comment flagged as sensitive?

Resource-based authorization naturally handles these hierarchies by examining the resource and its relationships.

## Multi-Tenancy

In multi-tenant applications, users from one tenant must never access another tenant's data. Every resource belongs to a tenant, and every access must verify tenant membership:

```csharp
public class TenantAuthorizationHandler<TResource> 
    : AuthorizationHandler<OperationAuthorizationRequirement, TResource>
    where TResource : ITenantResource
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        TResource resource)
    {
        var userTenantId = context.User.FindFirstValue("tenant_id");
        
        if (resource.TenantId != userTenantId)
        {
            // Wrong tenant - never authorize
            return Task.CompletedTask;
        }
        
        // Same tenant - continue with other authorization logic
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
```

## Performance Considerations

Resource-based authorization requires loading the resource before making an authorization decision. This can impact performance:

**Problem:** Fetching the entire Order entity just to check if the user owns it.

**Solution 1: Lightweight Queries**
Create a method that returns only the fields needed for authorization:
```csharp
var orderAuth = await _repo.GetOrderAuthInfoAsync(orderId);
// Returns: { CustomerId, SellerId, Status, CreatedAt }
```

**Solution 2: Query-Based Authorization**
Build authorization into the database query:
```csharp
var order = await _context.Orders
    .Where(o => o.Id == orderId)
    .Where(o => o.CustomerId == userId || isAdmin)
    .FirstOrDefaultAsync();
// Returns null if not authorized
```

**Solution 3: Caching**
Cache authorization results for repeated checks on the same resource:
```csharp
var cacheKey = $"auth:{userId}:{resourceType}:{resourceId}:{operation}";
```

## Combining with Role-Based Authorization

Resource-based authorization works best in combination with roles:

1. **First:** Policy-based check - 'Does user have permission to cancel orders in general?'
2. **Then:** Resource-based check - 'Does user have permission to cancel THIS order?'

This layered approach keeps handlers focused. The policy rejects users who cannot cancel any orders. The handler focuses on ownership and business rules for specific orders.