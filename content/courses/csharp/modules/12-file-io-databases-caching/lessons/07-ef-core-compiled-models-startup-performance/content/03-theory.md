---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`dotnet ef dbcontext optimize`**: CLI command that generates compiled model. Scans your DbContext, generates C# code representing the model. Traditional approach - run after schema changes!

**`Microsoft.EntityFrameworkCore.Tasks`**: EF Core 9 MSBuild task package. Enables auto-compiled models. Install via NuGet, set EFOptimizeContext=true in .csproj.

**`<EFOptimizeContext>true</EFOptimizeContext>`**: .csproj property that enables automatic model regeneration on every build. No more forgetting to run CLI command!

**`.UseModel(Model.Instance)`**: Tells EF to use pre-built model instead of discovering at runtime. Massive startup speedup for large models.

**Limitations**: Global Query Filters not compatible. No lazy-loading proxies. Must rebuild after entity changes (auto-compile handles this). Worth it for 100+ entities or cold-start sensitive apps.