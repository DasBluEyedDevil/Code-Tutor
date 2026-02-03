---
type: "KEY_POINT"
title: "Authentication vs Authorization"
---

## Key Takeaways

- **Authentication = WHO you are; Authorization = WHAT you can do** -- authentication verifies identity (login). Authorization checks permissions (admin access). Both are required but serve different purposes.

- **Choose the right scheme** -- cookies for browser-based web apps (automatic, supports sliding expiration). Bearer tokens for APIs (stateless, scalable). Each has tradeoffs for security and architecture.

- **ASP.NET Core supports multiple schemes simultaneously** -- configure cookie auth for your web UI and JWT bearer for your API in the same application. The `[Authorize]` attribute routes to the correct scheme.
