---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`dotnet add package PackageName`**: Install a NuGet package. Run in project folder. Adds to .csproj automatically. Latest version by default.

**`dotnet add package Name --version 8.0.0`**: Install specific version. Use when you need compatibility with older code or want to avoid breaking changes.

**`dotnet list package`**: Show all installed packages and their versions. Add `--outdated` to see which packages have updates available!

**`dotnet remove package Name`**: Uninstall a package. Removes from .csproj file.

**`dotnet restore`**: Download all packages listed in .csproj. Run after cloning a project or if packages are missing.

**`JsonSerializer.Serialize()`**: From System.Text.Json (built-in). Converts C# objects to JSON strings. Use JsonSerializerOptions { WriteIndented = true } for readable output.

**`JsonSerializer.Deserialize<T>()`**: Converts JSON string back to C# object. Specify type with <Person>. Returns null if JSON doesn't match structure - use ! operator when you know data is valid.