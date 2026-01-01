---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`MapIdentityApi<TUser>()`**: .NET 8/9 feature! Automatically creates /register, /login, /refresh, /confirmEmail endpoints. Uses bearer tokens by default (add ?useCookies=true for cookies).

**`AddIdentityApiEndpoints<TUser>()`**: Registers Identity services with token support. Includes password hashing, email confirmation, 2FA. Pair with .AddEntityFrameworkStores<TContext>().

**`UseAuthentication() + UseAuthorization()`**: Middleware order matters! Authentication MUST come before Authorization. Wrong order = authorization always fails!

**`.RequireAuthorization()`**: Protects endpoints. No parameters = any authenticated user. With policy: `RequireAuthorization(policy => policy.RequireRole("Admin"))`.

**`ClaimsPrincipal user`**: Injected parameter containing user info from token. Access claims: `user.FindFirst("ClaimType")?.Value`. Get name: `user.Identity?.Name`.

**`Bearer tokens vs Cookies`**: MapIdentityApi supports both! Use ?useCookies=false in /login for bearer tokens (APIs). Cookies for browser apps. Bearer tokens are stateless and scalable.