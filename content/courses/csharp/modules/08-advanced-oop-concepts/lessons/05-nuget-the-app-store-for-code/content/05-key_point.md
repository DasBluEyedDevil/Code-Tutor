---
type: "KEY_POINT"
title: "NuGet Package Management"
---

## Key Takeaways

- **`dotnet add package Name` installs packages** -- it adds the reference to your `.csproj` file automatically. Use `--version` to pin a specific version when needed.

- **`System.Text.Json` is built in** -- for JSON serialization, you do not need a NuGet package. `JsonSerializer.Serialize()` and `JsonSerializer.Deserialize<T>()` handle most scenarios.

- **Check packages before trusting them** -- verify download counts, last update date, and the publisher. Prefer packages from Microsoft, well-known open source maintainers, or the .NET Foundation.
