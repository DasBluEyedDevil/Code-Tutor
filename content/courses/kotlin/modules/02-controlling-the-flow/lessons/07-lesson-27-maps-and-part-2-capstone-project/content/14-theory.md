---
type: "THEORY"
title: "What's Next?"
---


In **Part 3: Functional Programming in Kotlin**, you'll level up with:
- Lambda expressions and higher-order functions
- Advanced collection operations
- Sequences for lazy evaluation
- Scope functions (let, apply, with, run, also)
- Function composition and chaining

**Preview:**

Get ready to write more expressive, concise, and powerful Kotlin code!

---

**🏆 Outstanding work completing Part 2! You're becoming a Kotlin developer!** 🎉



```kotlin
val numbers = listOf(1, 2, 3, 4, 5)

numbers
    .filter { it % 2 == 0 }
    .map { it * it }
    .forEach { println(it) }

val result = listOf("apple", "banana", "cherry")
    .filter { it.length > 5 }
    .map { it.uppercase() }
    .joinToString(", ")
```
