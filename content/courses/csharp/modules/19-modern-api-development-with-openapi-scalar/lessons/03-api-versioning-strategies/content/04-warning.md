---
type: "WARNING"
title: "Common Pitfalls"
---

**Separate Package Required**: API versioning is NOT built into .NET 9. You must install `Asp.Versioning.Http` package: `dotnet add package Asp.Versioning.Http`.

**AddApiExplorer() for OpenAPI**: If your versioned endpoints don't appear correctly in Scalar/Swagger, you forgot to chain `.AddApiExplorer()` after `AddApiVersioning()`. This is required for proper OpenAPI integration.

**Route Conflicts**: Having the same route path in multiple version groups without proper version constraints causes ambiguous route matching. Always use `HasApiVersion()` on groups or `MapToApiVersion()` on individual endpoints.

**Breaking Changes Need New Versions**: Removing fields, changing types, or modifying behavior are breaking changes. ALWAYS create a new major version (v2.0) instead of modifying v1.0. Existing clients depend on the old contract.

**Deprecation Strategy**: Don't just delete old versions. Mark them deprecated, give clients migration time (3-6 months typical), communicate the timeline, then sunset. Use `[ApiVersion("1.0", Deprecated = true)]` to signal deprecation.

**Version in URL vs Header**: URL versioning (`/api/v1/`) is most explicit and cacheable. Header versioning (`X-API-Version`) keeps URLs clean but can be harder to debug. Pick one primary strategy for consistency.