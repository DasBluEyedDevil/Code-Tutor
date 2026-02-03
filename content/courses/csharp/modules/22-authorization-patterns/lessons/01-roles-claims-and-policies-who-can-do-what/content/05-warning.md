---
type: "WARNING"
title: "Common Pitfalls"
---

## Authorization Pitfalls

**Confusing Authentication with Authorization**: Authentication answers "who are you?" and authorization answers "what can you do?" Returning 401 Unauthorized when you mean 403 Forbidden confuses API consumers. Use 401 when credentials are missing or invalid, 403 when authenticated but lacking permission.

**Hardcoded Role Strings**: Scattering `"Admin"` string literals across your codebase leads to silent failures when someone types `"admin"` (wrong case). Define roles as constants in a single class and reference those constants everywhere. A typo in a constant produces a compile error, not a runtime authorization bypass.

**Over-Restrictive Default Policies**: Setting a global fallback policy that requires authentication on ALL endpoints locks out health checks, OpenAPI documentation, login endpoints, and static files. Be explicit about which endpoints need protection rather than locking everything and punching holes with AllowAnonymous.

**Policy Evaluation Order**: When multiple authorization attributes are applied, they ALL must pass (AND logic). `[Authorize(Roles = "Admin")] [Authorize(Policy = "VerifiedEmail")]` requires Admin role AND verified email. If you want OR logic, create a single policy with custom requirements.
