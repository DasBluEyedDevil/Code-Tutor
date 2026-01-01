---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`<PublishAot>true</PublishAot>`**: Master switch for AOT. Only affects 'dotnet publish'. Development with 'dotnet run' still uses JIT for fast iteration.

**`<TrimMode>full</TrimMode>`**: Aggressive tree-shaking. Removes ALL code not provably used. Some reflection patterns may break - test thoroughly!

**`<EnableTrimAnalyzer>true</EnableTrimAnalyzer>`**: Static analysis for trim compatibility. Warns about patterns that might break. Fix these warnings!

**`<EnableAotAnalyzer>true</EnableAotAnalyzer>`**: Warns about AOT-incompatible patterns. Catches issues at compile time, not runtime.

**`[JsonSerializable(typeof(T))]`**: Source generator attribute. Tells JSON serializer to generate code for type T at compile time instead of using reflection.

**`JsonSerializerContext`**: Base class for source-generated JSON. AppJsonContext.Default.Config provides pre-generated serialization logic.

**`-r win-x64 / linux-x64`**: Runtime Identifier. Required for AOT because native code is platform-specific. Cross-compilation is supported!