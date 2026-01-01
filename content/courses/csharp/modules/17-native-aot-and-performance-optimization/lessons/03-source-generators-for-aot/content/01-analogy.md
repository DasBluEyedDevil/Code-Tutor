---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine you run a custom t-shirt printing shop. You have TWO business models:

ON-DEMAND PRINTING (Reflection at Runtime):
- Customer orders a shirt
- You design it on the spot
- Print it while they wait
- Flexible but slow
- Requires full printing equipment everywhere

PRE-PRINTED CATALOG (Source Generators):
- Before opening, design ALL possible shirts
- Print them in advance
- Customer picks from ready stock
- Instant delivery, no equipment needed at stores

SOURCE GENERATORS:
- Run during compilation (not runtime)
- Generate C# code that gets compiled with your app
- No reflection needed at runtime
- Perfect for AOT because all code exists at compile time

BUILT-IN SOURCE GENERATORS:

1. JSON Source Generator:
   - [JsonSerializable(typeof(T))]
   - Generates serialization code for your types
   - No reflection for JSON parsing!

2. Regex Source Generator:
   - [GeneratedRegex(pattern)]
   - Compiles regex at build time
   - Faster than runtime compilation

3. Logging Source Generator:
   - [LoggerMessage(...)]
   - Generates high-performance logging
   - Zero allocations for log messages

WHY SOURCE GENERATORS MATTER FOR AOT:
- AOT can't generate code at runtime
- Source generators move that work to compile time
- Result: All code exists in the final binary

Think: 'Source generators are like pre-cooking meals - all the work happens in the kitchen, so serving is instant!'