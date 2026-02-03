---
type: "WARNING"
title: "Common Pitfalls"
---

## Production Readiness Pitfalls

**Capstone Scope Creep**: The ShopFlow capstone is designed to demonstrate specific skills from each module. Adding "just one more feature" (payment processing, email notifications, admin dashboards) delays completion and dilutes focus. Ship the required features first, then iterate. A deployed MVP teaches more than an unfinished masterpiece.

**Skipping the Production Checklist**: Developers often jump from "it works locally" to "let's deploy." Production requires HTTPS enforcement, proper CORS configuration, environment-specific settings, error pages instead of stack traces, and logging configured for a monitoring service. Walk through the checklist in this lesson before deploying.

**Missing Environment-Specific Configuration**: Using `appsettings.Development.json` connection strings in production is a surprisingly common mistake. Verify that `ASPNETCORE_ENVIRONMENT` is set to `Production` on your deployment target, and that all sensitive configuration comes from environment variables or a secret manager -- never from files committed to source control.
