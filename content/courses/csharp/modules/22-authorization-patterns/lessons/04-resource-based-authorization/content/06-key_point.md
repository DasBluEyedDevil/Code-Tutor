---
type: "KEY_POINT"
title: "Resource-Based Authorization"
---

## Key Takeaways

- **Resource-based authorization checks ownership** -- "Can this user edit THIS order?" requires knowing both the user and the specific resource. Roles alone cannot express ownership.

- **Implement `IAuthorizationHandler`** -- create a handler that receives the resource and user, then checks ownership: `resource.OwnerId == user.FindFirst(ClaimTypes.NameIdentifier)?.Value`.

- **Call `IAuthorizationService.AuthorizeAsync()` in your endpoint** -- pass the resource and policy name. Return `Forbid()` if authorization fails. This happens after the resource is loaded, unlike attribute-based authorization.
