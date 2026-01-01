---
type: "THEORY"
title: "Type Inference"
---


Kotlin is smart—it can figure out types automatically!


**When to use explicit types**:
- When the type isn't obvious
- For documentation/clarity
- Most of the time, let Kotlin infer!


---



```kotlin
// Inference is clear
val count = 10  // Obviously Int

// Explicit might help readability
val result: Boolean = checkStatus()  // Makes intent clear
```
