---
type: "ANALOGY"
title: "The Concept"
---


### The Box Analogy

Think of variables as boxes that can hold values:

**Regular Box (Non-Nullable)**:
- Must always contain something
- Opening it always gives you a value
- Safe to use anytime


**Special Box (Nullable)**:
- Might contain something, might be empty
- Must check before using
- Prevents surprises


---



```kotlin
val name: String? = null  // Box might be empty
// println(name.length)  // ❌ Compiler error: might be null!
println(name?.length)  // ✅ Safe: checks first
```
