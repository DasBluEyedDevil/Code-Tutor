---
type: "KEY_POINT"
title: "Configuring AOT in .csproj"
---

## Key Takeaways

- **Enable both trim and AOT analyzers** -- `<EnableTrimAnalyzer>true</EnableTrimAnalyzer>` and `<EnableAotAnalyzer>true</EnableAotAnalyzer>` catch compatibility issues at compile time, not at runtime in production.

- **`<TrimMode>full</TrimMode>` aggressively removes unused code** -- this minimizes binary size but can break reflection-based patterns. Fix all analyzer warnings before publishing.

- **Test AOT builds in CI** -- add a `dotnet publish` step to your CI pipeline that targets your deployment platform. Some NuGet packages are not AOT-compatible; discover this early, not during deployment.
