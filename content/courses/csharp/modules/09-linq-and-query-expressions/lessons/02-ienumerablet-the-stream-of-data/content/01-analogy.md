---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine a conveyor belt in a factory. Items move along one at a time. You don't need ALL items in memory - you process each one as it arrives!

That's IEnumerable<T> - it represents a SEQUENCE of items:
• 'T' is the type: IEnumerable<int>, IEnumerable<string>
• Items are accessed ONE AT A TIME (via foreach)
• Doesn't load everything into memory at once
• LINQ methods return IEnumerable<T>

Why use it?
• MEMORY EFFICIENT: Query 1 million items without loading them all
• LAZY EVALUATION: Computation happens only when needed
• FLEXIBLE: Works with any collection type

List<T> implements IEnumerable<T>, arrays implement it, LINQ results are IEnumerable<T>.

Think: IEnumerable<T> = 'A promise of future items, delivered one at a time when you ask.'