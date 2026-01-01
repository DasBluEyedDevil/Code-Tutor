---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the CLR working behind the scenes. When you run this code, .NET compiles it to intermediate language (IL), then the CLR's JIT compiler converts it to machine code for your specific computer.

```csharp
// The CLR is the engine powering your C# programs!

// Step 1: You write C# code (what you're doing now)
Console.WriteLine("Hello from C#!");

// Step 2: The compiler turns this into IL (Intermediate Language)
// Step 3: The CLR's JIT (Just-In-Time) compiler converts IL to machine code
// Step 4: Your computer runs the machine code!

// The CLR also provides:
Console.WriteLine("- Automatic memory management (no manual cleanup!)");
Console.WriteLine("- Type safety (catches errors before they crash)");
Console.WriteLine("- Security features (protects against malicious code)");
Console.WriteLine("- Exception handling (graceful error recovery)");

// You're using .NET 9 - released November 2024!
// It includes the latest CLR with improved performance
// and Native AOT for faster startup times.
```
