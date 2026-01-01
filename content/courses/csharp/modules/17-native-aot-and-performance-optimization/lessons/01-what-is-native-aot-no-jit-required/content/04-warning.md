---
type: "WARNING"
title: "Common Pitfalls"
---

## Critical AOT Limitations

**Reflection Limitations**: Native AOT has significant reflection restrictions. Code using `typeof(T).GetProperties()`, `Activator.CreateInstance()`, or dynamic type loading may fail at runtime. Use source generators or compile-time alternatives instead.

**No Runtime Code Generation**: The `dynamic` keyword, `Reflection.Emit`, and runtime expression compilation are NOT supported in AOT. These require JIT which doesn't exist in native binaries.

**Platform-Specific Binaries**: Unlike .NET's 'build once, run anywhere' model, AOT produces separate binaries for each target platform. You must build and deploy for each OS/architecture combination.

**.NET 9 Memory Improvements**: Native AOT apps in .NET 9 use 30-40% less memory than previous versions. However, initial binary sizes may still be larger than JIT deployments - typically 10-15 MB minimum.

**Testing is Essential**: Always test your AOT builds thoroughly! Code that works perfectly with JIT may fail in AOT due to trimming or reflection issues. Enable `<EnableAotAnalyzer>true</EnableAotAnalyzer>` to catch issues early.