---
type: "THEORY"
title: "What is the K2 Compiler?"
---


### The K2 Revolution

The K2 compiler is a complete rewrite of Kotlin's compiler frontend. The name "K2" comes from it being the second major version of the Kotlin compiler.

**Why Rewrite?**

The original Kotlin compiler (K1) was built incrementally over many years. As Kotlin evolved, the compiler accumulated technical debt:

1. **Performance bottlenecks** - Some analysis phases couldn't be parallelized
2. **Complex codebase** - Hard to add new features
3. **Incremental compilation limitations** - More rebuilds than necessary
4. **IDE integration challenges** - Separate analysis for compiler and IDE

**K2 Architecture**

K2 uses a new architecture called FIR (Frontend IR):

- **Unified representation** - Same data structures for compiler and IDE
- **Lazy analysis** - Only analyze what's needed
- **Better parallelization** - Analyze files concurrently
- **Cleaner design** - Easier to extend and maintain

---

