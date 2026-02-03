---
type: "WARNING"
title: "Compiled Models Pitfalls"
---

## Watch Out For These Issues!

**Stale compiled models**: If you change entity classes or DbContext configuration but forget to regenerate compiled models, the app uses the OLD schema. This causes runtime exceptions or silent data issues. The EF Core 9 auto-compiled models feature (`EFOptimizeContext=true` in .csproj) eliminates this risk by regenerating on every build.

**Global query filters not supported**: Compiled models do NOT support global query filters (`HasQueryFilter()` in OnModelCreating). If your app uses soft-delete patterns (`.HasQueryFilter(e => !e.IsDeleted)`), compiled models will ignore these filters. Check your app requirements before enabling!

**Premature optimization**: For small models (under 50 entities), compiled models add build complexity with negligible startup improvement. Only use them when cold-start time is measurable and significant (microservices, serverless functions, large models with 100+ entities).

**Build time increase**: Auto-compiled models regenerate on every build. For very large models, this can noticeably slow down your build. Monitor your build times after enabling the feature and consider disabling for debug builds if needed.
