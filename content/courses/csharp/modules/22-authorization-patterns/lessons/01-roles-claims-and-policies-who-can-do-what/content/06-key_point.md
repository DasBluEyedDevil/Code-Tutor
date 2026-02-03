---
type: "KEY_POINT"
title: "Authorization Model Selection"
---

## Key Takeaways

- **Roles for broad access, Claims for fine-grained control, Policies for complex rules** -- roles answer "Is this user an Admin?" Claims answer "Does this user have premium access?" Policies combine multiple requirements.

- **Start with roles, add claims when roles are not enough** -- if you find yourself creating roles like "AdminReadOnly" or "ManagerWithReporting," you need claims-based authorization instead.

- **Policies are reusable authorization rules** -- `builder.Services.AddAuthorization(o => o.AddPolicy("CanEditProducts", p => p.RequireClaim("permission", "products:edit")))` defines the rule once. Apply with `[Authorize(Policy = "CanEditProducts")]`.
