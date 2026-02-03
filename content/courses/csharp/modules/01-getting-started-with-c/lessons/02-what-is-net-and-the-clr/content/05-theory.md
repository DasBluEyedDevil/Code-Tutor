---
type: "THEORY"
title: "Inside the CLR: How Your Code Actually Runs"
---

## The Compilation Pipeline

When you run `dotnet build`, your C# code goes through multiple transformations before it can run on your computer. Understanding this pipeline helps you debug issues and make informed performance decisions.

**1. Roslyn Compiler (C# → IL)**
The Roslyn compiler parses your C# source code and converts it to Intermediate Language (IL), also called MSIL or CIL. IL is a platform-independent bytecode—think of it as a universal language that any .NET runtime can understand. This compiled IL is stored in .dll or .exe files. The beauty of IL is that you write code once, and it can run on Windows, Linux, or macOS without recompilation.

**2. Just-In-Time Compilation (IL → Machine Code)**
When your application starts, the CLR's JIT (Just-In-Time) compiler converts IL to native machine code specific to your CPU architecture (x64, ARM64, etc.). The JIT compiler is smart—it only compiles methods when they're first called, not the entire application upfront.

.NET 9 includes Dynamic PGO (Profile-Guided Optimization), which observes how your code actually runs and recompiles hot paths with better optimizations. This means your application gets faster the longer it runs, as the JIT learns from real usage patterns.

**3. Native AOT (Ahead-of-Time Compilation)**
.NET 9 also supports Native AOT, which compiles directly to native machine code at build time, completely bypassing the JIT. This eliminates startup latency (great for serverless functions or CLI tools) but produces larger binaries and loses some runtime flexibility. Choose JIT for long-running services; choose AOT for quick-start scenarios.

**4. Garbage Collection**
The CLR automatically manages memory through garbage collection (GC). When objects are no longer referenced, the GC reclaims that memory. .NET's GC is highly tuned—it uses generational collection (Gen0, Gen1, Gen2) because most objects die young. You rarely need to think about memory management, but understanding GC helps when optimizing performance-critical code.

**Why This Matters**
Understanding the compilation pipeline helps you:
- Debug assembly-related errors and version conflicts
- Choose between JIT (flexibility) and AOT (startup speed) for deployment
- Understand performance profiling results
- Make informed decisions about memory usage

**Viewing IL Code**
Curious what your C# becomes? Install the IL decompiler:
`dotnet tool install -g ilspycmd`

Then inspect any assembly:
`ilspycmd YourApp.dll`

You'll see the intermediate representation that the CLR actually executes—a valuable learning experience!