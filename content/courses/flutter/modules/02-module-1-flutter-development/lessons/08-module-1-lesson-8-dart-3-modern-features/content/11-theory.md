---
type: "THEORY"
title: "Part 3: Sealed Classes - Exhaustive Type Hierarchies"
---


### What Are Sealed Classes?

**Conceptual First:**
Imagine a traffic light. It can ONLY be red, yellow, or green - nothing else. If you handle all three cases, you've covered everything possible.

**Sealed classes** let you define a closed set of types. The compiler then ensures you handle ALL cases - no more forgotten edge cases!

**Jargon:**
- **Sealed class**: A class that can only be extended within the same file
- **Exhaustive switch**: A switch that handles all possible subtypes
- **Algebraic data types (ADTs)**: Types representing one of several possible variants

