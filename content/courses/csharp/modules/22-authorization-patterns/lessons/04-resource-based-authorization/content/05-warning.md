---
type: "WARNING"
title: "Common Pitfalls"
---

## Resource-Based Authorization Pitfalls

**Forgetting Resource-Level Checks**: Role-based authorization only checks WHO the user is, not WHAT they are accessing. A user with the "Customer" role can see their own orders, but without resource-based checks, they can also see every other customer's orders by changing the ID in the URL. Always verify ownership at the resource level.

**N+1 Authorization Queries**: Calling `IAuthorizationService.AuthorizeAsync()` inside a loop for each item in a list triggers a database query per item. For list endpoints, filter the query at the database level (e.g., `WHERE CustomerId = @currentUserId`) instead of fetching all records and checking authorization one by one.

**Inconsistent Authorization Logic**: When the same resource-access check exists in the controller AND the service layer AND a custom middleware, they inevitably drift apart. Centralize resource-based authorization in IAuthorizationHandler implementations and invoke them from a single location. One source of truth prevents bypass paths.

**Missing Authorization on Related Endpoints**: Protecting `/api/orders/{id}` but leaving `/api/orders/{id}/items` or `/api/orders/{id}/invoice` unprotected exposes data through related endpoints. Audit all endpoints that expose data from the same aggregate to ensure consistent authorization coverage.
