---
type: "THEORY"
title: "Designing Permission Systems"
---

## The Permission Matrix

Before writing code, map out your authorization requirements in a permission matrix. For ShopFlow, we identify resources (rows) and actions (columns), then fill in which roles or conditions grant access:

```
                    | View   | Create | Edit   | Delete | Manage |
--------------------|--------|--------|--------|--------|--------|
Products            | All    | Mgr+   | Mgr+   | Admin  | Admin  |
Orders              | Auth   | Cust   | Owner* | Admin  | Admin  |
Customer Profiles   | Self   | Self   | Self   | Admin  | Admin  |
Inventory           | Mgr+   | Mgr+   | Mgr+   | Admin  | Admin  |
Reports             | Mgr+   | -      | -      | -      | Admin  |
Settings            | Admin  | Admin  | Admin  | Admin  | Admin  |
```

Legend: All=Everyone, Auth=Authenticated, Cust=Customer+, Mgr+=Manager+Admin, Owner*=Resource-based, Self=Own profile only

This matrix immediately reveals patterns: Admin can do everything, Manager has operational access, Customer has limited self-service capabilities. Resource-based checks (Owner*) require special handling.

## Translating to Policies

From the matrix, extract named policies that encode these rules:

**Role-Based Policies:**
- `RequireAdmin` - Admin role only
- `RequireManager` - Manager or Admin role
- `RequireAuthenticated` - Any authenticated user

**Composite Policies:**
- `CanManageProducts` - RequireManager AND department in (Products, Inventory)
- `CanViewReports` - RequireManager AND (department matches report type OR Admin)
- `CanProcessOrders` - RequireAuthenticated AND (Customer placing own order OR Manager/Admin)

**Resource-Based Authorization Handlers:**
- `OrderAuthorizationHandler` - Check if user owns the order or has Manager+ role
- `ProfileAuthorizationHandler` - Check if user is accessing their own profile or is Admin

## ShopFlow Authorization Architecture

For ShopFlow, we implement a layered authorization system:

**Layer 1: Global Policies (Program.cs)**
Define policies at application startup. These are your authorization vocabulary - named rules that can be referenced anywhere.

**Layer 2: Endpoint Authorization**
Apply policies to API endpoints using [Authorize] attributes or .RequireAuthorization(). This is your first line of defense, rejecting unauthorized requests early.

**Layer 3: Resource Authorization Handlers**
For operations on specific resources (edit this order, view this profile), implement IAuthorizationHandler. These handlers receive the resource and make context-aware decisions.

**Layer 4: Business Logic Checks**
Some authorization is so tightly coupled to business rules that it lives in service classes. 'Can this order be cancelled?' might depend on order status, payment state, and shipping status - logic that belongs in OrderService, not an authorization handler.

## Custom Authorization Requirements

ASP.NET Core's authorization is built on Requirements and Handlers. A requirement is a marker class that defines what we're checking. A handler contains the logic to evaluate that requirement:

```csharp
// Requirement: User must be in the same department as the resource
public class SameDepartmentRequirement : IAuthorizationRequirement { }

public class SameDepartmentHandler : AuthorizationHandler<SameDepartmentRequirement, Product>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        SameDepartmentRequirement requirement,
        Product resource)
    {
        var userDept = context.User.FindFirst("department")?.Value;
        if (userDept == resource.Department || context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
```

## Performance Considerations

Authorization checks happen on every request. Keep them fast:

- **Cache role/claim lookups** when possible
- **Avoid database calls** in authorization handlers when you can use claims
- **Load resources efficiently** - don't fetch the entire order to check ownership, just query the owner ID
- **Use policy results caching** for expensive policy evaluations
- **Fail fast** - check the cheapest requirements first (roles before database lookups)

## Audit and Compliance

For regulated industries, authorization decisions must be auditable:

- Log authorization failures with context (who, what, when, why denied)
- Consider logging successful sensitive-resource access
- Implement permission change tracking (who granted Admin role, when)
- Regular access reviews (does user still need Manager role after department change?)

The permission matrix becomes your compliance documentation - it explicitly defines who can do what, making audits straightforward.