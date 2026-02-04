---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Authenticate routes by installing JWT validation middleware** that runs before your handler logic. Use Ktor's `authenticate` block to declaratively protect routes without repeating auth checks.

**Extract user identity from validated JWTs via `principal<JWTPrincipal>()`**. The authentication plugin populates the principal after successful validation, giving you type-safe access to claims.

**Implement authorization (permissions) separately from authentication (identity)**. Just because a user is authenticated doesn't mean they can access every resource—check ownership and roles before allowing operations.
