---
type: "KEY_POINT"
title: "AOT-Compatible Minimal APIs"
---

## Key Takeaways

- **Use `CreateSlimBuilder()` for AOT APIs** -- it excludes features not needed for minimal APIs, producing smaller binaries. Pair it with your `JsonSerializerContext` for AOT-safe JSON serialization.

- **Register JSON context explicitly** -- `ConfigureHttpJsonOptions(o => o.TypeInfoResolverChain.Insert(0, AppJsonContext.Default))` ensures all endpoints use source-generated serialization instead of reflection.

- **Minimal APIs are fully AOT-compatible in .NET 9** -- the combination of `CreateSlimBuilder`, source-generated JSON, and typed results produces APIs that start in under 10ms with tiny memory footprints.
