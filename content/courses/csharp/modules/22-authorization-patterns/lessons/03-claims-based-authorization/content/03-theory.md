---
type: "THEORY"
title: "When to Use Claims vs Roles"
---

## Roles: Coarse-Grained Access Control

Roles work best when you need to categorize users into broad permission groups that align with business functions. Think of roles as job titles that naturally bundle sets of permissions together. An 'Admin' role implies full system access. A 'Seller' role implies product management capabilities. A 'Customer' role implies shopping and order history access.

Use roles when:
- Permissions map cleanly to organizational hierarchy
- A single role assignment should grant many related permissions
- You have relatively few distinct permission levels (typically 3-7 roles)
- Authorization decisions are simple: 'Is this user a Manager?'

Roles become problematic when:
- You need fine-grained control ('can edit products but not delete them')
- Permission requirements vary by feature rather than by user type
- You find yourself creating many hybrid roles ('ManagerWithReporting', 'AdminReadOnly')

## Claims: Fine-Grained Permissions

Claims shine when you need granular, feature-level permissions that do not map neatly to organizational roles. Instead of 'Admin can do everything', claims let you express 'User has Products.Read, Products.Edit, but not Products.Delete'. Claims are facts about users that policies evaluate.

Use claims when:
- Different features require different permissions
- You need permission combinations that cross role boundaries
- External identity providers supply user attributes
- Authorization logic involves user properties (subscription tier, department, clearance level)
- You want to grant specific capabilities without creating new roles

## Common Claim Patterns

**Permission Claims:** Grant specific capabilities
- `permission:Products.Read`
- `permission:Products.Edit`
- `permission:Orders.Refund`

**Feature Access Claims:** Control feature availability
- `feature_access:analytics`
- `feature_access:bulk_upload`
- `feature_access:api_v2`

**Attribute Claims:** Describe user properties for policy evaluation
- `account_tier:premium`
- `department:Engineering`
- `clearance_level:3`

**Limit Claims:** Define usage boundaries
- `max_products:1000`
- `api_rate_limit:10000`
- `storage_quota_gb:50`

## Combining Roles and Claims

The most effective authorization systems combine both approaches. Roles provide coarse-grained baseline permissions, while claims add fine-grained customization. A 'Seller' role might grant basic product management, but claims determine specific capabilities like 'bulk_upload' or 'analytics'. This keeps role count manageable while enabling precise permission control.

Example flow:
1. User registers and receives 'Customer' role
2. User subscribes to premium tier, receives `account_tier:premium` claim
3. User becomes a seller, receives 'Seller' role
4. Admin grants analytics access, adds `feature_access:analytics` claim
5. Authorization policies check both: 'Require Seller role AND analytics feature claim'

## The Permission Claim Pattern

A popular pattern is using permission claims with a consistent naming convention:

```csharp
public static class Permissions
{
    public const string ProductsRead = "Products.Read";
    public const string ProductsEdit = "Products.Edit";
    public const string ProductsDelete = "Products.Delete";
    public const string OrdersView = "Orders.View";
    public const string OrdersProcess = "Orders.Process";
    public const string OrdersRefund = "Orders.Refund";
}

// Policy for each permission
options.AddPolicy("CanEditProducts", policy =>
    policy.RequireClaim("permission", Permissions.ProductsEdit));
```

This approach scales well. New features add new permission claims. Users receive exactly the permissions they need. Policies remain simple 'require this permission' checks.