---
type: "WARNING"
title: "Common Pitfalls"
---

## Critical Warnings for .NET Aspire

**Docker/Podman Required**: Aspire uses containers for local infrastructure (Redis, Postgres, etc.). Ensure Docker Desktop or Podman is running BEFORE starting AppHost, or you'll get cryptic container errors.

**Project Reference Syntax**: Use `AddProject<Projects.MyApi>("name")` NOT `AddProject("MyApi")`. The generic syntax is compile-time checked and auto-generated from your solution.

**WaitFor for Dependencies**: In Aspire 9.0+, use `.WaitFor(db)` to ensure services start AFTER their dependencies are healthy. Without it, your API might start before Postgres is ready!

**Local vs Production Differences**: Aspire abstracts infrastructure, but behavior differs. Local Redis runs in a container; production might use Azure Cache for Redis. Test in production-like environments!

**Dashboard Port Conflicts**: The Aspire dashboard defaults to port 18888. If blocked, check for other Aspire instances or configure via `DOTNET_DASHBOARD_FRONTEND_BROWSERTOKEN` environment variable.

**Memory Consumption**: Running multiple containers locally consumes significant RAM. Monitor Docker memory limits if you experience slowdowns or crashes.