---
type: "ANALOGY"
title: "The Concept: Building with Functions"
---


### The LEGO Analogy

Imagine building with LEGO:
- **Small pieces**: Individual functions (single responsibility)
- **Combining pieces**: Function composition (build complex structures)
- **Specialized tools**: Extension functions, operators


**Better with composition**:


---



```kotlin
val process = ::trim then ::uppercase then ::addExclamation
val result = process("  hello  ")
println(result)  // HELLO!
```
