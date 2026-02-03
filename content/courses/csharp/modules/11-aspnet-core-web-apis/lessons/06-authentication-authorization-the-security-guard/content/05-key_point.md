---
type: "KEY_POINT"
title: "Identity API Endpoints"
---

## Key Takeaways

- **`MapIdentityApi<TUser>()` auto-generates auth endpoints** -- .NET 8/9 creates `/register`, `/login`, `/refresh`, and more with a single line. No need to write authentication boilerplate from scratch.

- **Middleware order matters: Authentication before Authorization** -- `UseAuthentication()` must come before `UseAuthorization()`. Wrong order means authorization always fails because the user identity is not yet established.

- **`.RequireAuthorization()` protects endpoints** -- add it to any endpoint to require authentication. Pass a policy for finer control: `.RequireAuthorization(p => p.RequireRole("Admin"))`.
