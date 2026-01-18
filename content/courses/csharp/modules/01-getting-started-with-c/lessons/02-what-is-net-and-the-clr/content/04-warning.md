---
type: "WARNING"
title: "Common Pitfalls"
---

## Important Things to Remember

**Don't confuse .NET Framework with .NET**: .NET Framework (older, Windows-only) is different from modern .NET (cross-platform). We use .NET 9!

**.NET versions matter**: Code written for .NET 9 might use features unavailable in older versions. Always check your target framework.

**The CLR is invisible but essential**: If you see errors like 'CLR exception' or 'runtime error', it means something went wrong while your code was running - not during compilation.

**Cross-platform doesn't mean identical**: While .NET 9 runs on Windows, Mac, and Linux, some features (like Windows Forms) are platform-specific.
