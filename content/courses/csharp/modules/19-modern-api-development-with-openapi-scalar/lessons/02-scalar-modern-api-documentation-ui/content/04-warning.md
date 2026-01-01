---
type: "WARNING"
title: "Common Pitfalls"
---

**Order Matters**: Always call `app.MapOpenApi()` BEFORE `app.MapScalarApiReference()`. Scalar reads from the OpenAPI endpoint - if it doesn't exist yet, you'll get an empty documentation page with no helpful error message.

**Separate NuGet Package**: Unlike the built-in OpenAPI support, Scalar requires installing `Scalar.AspNetCore` from NuGet. Don't forget: `dotnet add package Scalar.AspNetCore`.

**Development Only**: Consider wrapping Scalar in an environment check: `if (app.Environment.IsDevelopment()) { app.MapScalarApiReference(); }`. You typically don't want to expose API documentation in production.

**URL Path Confusion**: Scalar UI is at `/scalar/v1` by default, while the raw OpenAPI spec is at `/openapi/v1.json`. Don't confuse these - browsers should visit `/scalar/v1` for the interactive UI.

**Theme Persistence**: The theme and dark mode settings are defaults. Users can change them in the UI, but those preferences may not persist across sessions depending on browser settings.

**Missing using Statement**: If `WithTitle`, `WithTheme`, etc. don't compile, you're missing `using Scalar.AspNetCore;` at the top of your file.