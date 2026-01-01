---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`<PublishAot>true</PublishAot>`**: The magic switch! Tells the .NET SDK to compile to native code instead of IL. Only affects 'dotnet publish', not 'dotnet run'.

**`<InvariantGlobalization>true</InvariantGlobalization>`**: Disables culture-specific formatting (dates, numbers). Reduces binary size significantly. Use when you don't need localization.

**`<OptimizationPreference>Size</OptimizationPreference>`**: Optimize for smaller binary size instead of speed. Options: Speed (default), Size, or blank for balanced. Note: Previously called IlcOptimizationPreference in earlier .NET versions.

**`-r win-x64 / linux-x64 / osx-arm64`**: Runtime Identifier (RID). AOT produces platform-specific binaries. You must specify the target platform.

**Single File Output**: AOT produces ONE executable file. No DLLs, no runtime folder. Just copy and run!

**Startup Time**: AOT apps start in milliseconds because there's no JIT compilation. Perfect for CLI tools, serverless functions, microservices.