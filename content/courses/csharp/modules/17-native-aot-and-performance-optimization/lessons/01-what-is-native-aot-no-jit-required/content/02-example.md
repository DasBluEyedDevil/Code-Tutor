---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== COMPARING JIT vs NATIVE AOT =====

// TRADITIONAL .NET (JIT)
// dotnet run
// 1. Load .NET runtime
// 2. Load IL assemblies
// 3. JIT compile methods on first call
// 4. Execute
// Startup: ~100-500ms for simple apps

// NATIVE AOT
// dotnet publish -c Release
// 1. Load native executable
// 2. Execute
// Startup: ~10-50ms for simple apps!

// ===== ENABLING NATIVE AOT =====
// In your .csproj file:

// <Project Sdk="Microsoft.NET.Sdk">
//   <PropertyGroup>
//     <OutputType>Exe</OutputType>
//     <TargetFramework>net9.0</TargetFramework>
//     
//     <!-- Enable Native AOT -->
//     <PublishAot>true</PublishAot>
//     
//     <!-- Optional: Further reduce size -->
//     <InvariantGlobalization>true</InvariantGlobalization>
//     <OptimizationPreference>Size</OptimizationPreference>
//   </PropertyGroup>
// </Project>

// ===== SIMPLE AOT EXAMPLE =====
using System.Text.Json;

Console.WriteLine("Native AOT Demo!");
Console.WriteLine($"Process ID: {Environment.ProcessId}");
Console.WriteLine($"64-bit: {Environment.Is64BitProcess}");

// This works great in AOT!
var numbers = new[] { 1, 2, 3, 4, 5 };
var sum = numbers.Sum();
Console.WriteLine($"Sum: {sum}");

// Simple JSON (but needs source generators for AOT!)
var person = new Person("Alice", 30);
Console.WriteLine($"Person: {person.Name}, Age {person.Age}");

public record Person(string Name, int Age);

// ===== PUBLISH COMMANDS =====

// Development (JIT)
// dotnet run

// Production AOT (single file!)
// dotnet publish -c Release -r win-x64
// dotnet publish -c Release -r linux-x64
// dotnet publish -c Release -r osx-arm64

// ===== OUTPUT COMPARISON =====
// JIT publish:    ~80 MB (with runtime)
// AOT publish:    ~10-15 MB (self-contained)
// AOT trimmed:    ~3-8 MB (minimal)

// ===== MEMORY COMPARISON =====
// JIT startup:    ~50-100 MB working set
// AOT startup:    ~10-20 MB working set

Console.WriteLine("\nNative AOT Benefits:");
Console.WriteLine("- Instant startup (no JIT warmup)");
Console.WriteLine("- Smaller memory footprint");
Console.WriteLine("- Single file deployment");
Console.WriteLine("- No .NET runtime required on target");
```
