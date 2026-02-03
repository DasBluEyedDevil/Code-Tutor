---
type: "KEY_POINT"
title: "Compiled Models for Startup Performance"
---

## Key Takeaways

- **Compiled models pre-generate the EF Core model at build time** -- instead of discovering entities at startup, the compiled model is loaded instantly. This dramatically reduces cold-start time for large models (100+ entities).

- **EF Core 9 automates compilation with MSBuild tasks** -- set `<EFOptimizeContext>true</EFOptimizeContext>` in your .csproj and the model regenerates on every build. No manual CLI commands needed.

- **Know the limitations** -- compiled models do not support global query filters or lazy-loading proxies. Evaluate the tradeoffs for your specific application before enabling.
