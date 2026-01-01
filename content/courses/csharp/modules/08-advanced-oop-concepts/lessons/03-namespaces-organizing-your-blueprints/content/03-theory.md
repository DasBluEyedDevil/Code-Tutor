---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`namespace Name { }`**: Namespace wraps your classes. Use PascalCase. Dot notation for hierarchy: Company.Product.Feature (like folder paths!).

**`namespace Name;`**: FILE-SCOPED namespace (C# 10+). Applies to entire file - no braces or indentation needed! Cleaner for single-namespace files.

**`using Namespace;`**: 'using' at top of file imports a namespace. Now you can use classes from that namespace without full path. Like 'import' in other languages.

**`global using Namespace;`**: GLOBAL USING (C# 10+). Put in GlobalUsings.cs - applies to ALL files in project! No need to repeat common usings everywhere.

**`<ImplicitUsings>enable</ImplicitUsings>`**: In .csproj enables IMPLICIT USINGS. .NET 6+ projects auto-include System, System.Linq, System.Collections.Generic, etc. See generated file in obj/ folder.

**`Fully qualified name`**: Full path to a class: 'System.Collections.Generic.List<int>'. Using statements let you skip the path: just 'List<int>'.

**`Namespace organization`**: Convention: YourApp.Models (data classes), YourApp.Services (business logic), YourApp.Views (UI). Organize by feature or layer!