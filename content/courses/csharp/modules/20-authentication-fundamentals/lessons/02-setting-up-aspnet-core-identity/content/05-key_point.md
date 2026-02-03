---
type: "KEY_POINT"
title: "ASP.NET Core Identity Setup"
---

## Key Takeaways

- **Identity provides the full user lifecycle** -- `UserManager<TUser>` handles creation, password hashing, email confirmation, lockout, and two-factor authentication. You do not need to implement these from scratch.

- **`SignInManager<TUser>` manages authentication state** -- `PasswordSignInAsync()` validates credentials. `SignOutAsync()` ends the session. It handles lockout counting and two-factor challenges automatically.

- **Configure password and lockout policies explicitly** -- set `RequireDigit`, `RequiredLength`, `MaxFailedAccessAttempts`, and `LockoutTimeSpan` in `AddIdentity()` options. The defaults may be too weak for production.
