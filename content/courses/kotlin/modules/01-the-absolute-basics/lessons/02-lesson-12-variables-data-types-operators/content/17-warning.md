---
type: "WARNING"
title: "Common Mistakes and How to Avoid Them"
---


### Mistake 1: Integer Division Surprise


### Mistake 2: Trying to Reassign val


### Mistake 3: Type Mismatch


### Mistake 4: NumberFormatException


---



```kotlin
// ❌ Crashes if user types non-number
val number = readln().toInt()  // User types "hello" → crash!

// ✅ Safe conversion
val number = readln().toIntOrNull() ?: 0  // Returns 0 if invalid
```
