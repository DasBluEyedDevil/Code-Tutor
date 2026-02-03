---
type: "KEY_POINT"
title: "Namespace Organization"
---

## Key Takeaways

- **File-scoped namespaces (C# 10+) reduce nesting** -- `namespace MyApp.Services;` applies to the entire file with no braces needed. Use this form for all new code.

- **`global using` eliminates repetitive imports** -- place common usings in a `GlobalUsings.cs` file. .NET 6+ projects with `<ImplicitUsings>enable</ImplicitUsings>` auto-include System, System.Linq, and others.

- **Organize by feature or layer** -- `MyApp.Models`, `MyApp.Services`, `MyApp.Controllers`. Consistent namespace structure makes large codebases navigable and keeps related classes together.
