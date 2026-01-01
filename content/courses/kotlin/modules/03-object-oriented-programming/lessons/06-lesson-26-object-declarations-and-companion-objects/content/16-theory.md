---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) A singleton pattern with exactly one instance**

Object declarations create singletons - classes with exactly one instance that's created lazily.


---

**Question 2: B) An object that provides static-like members for a class**

Companion objects give you "static" functionality in Kotlin.


---

**Question 3: C) On first access (lazy initialization)**

Objects are created the first time they're accessed, not when the program starts.


---

**Question 4: C) Yes, multiple interfaces**

Companion objects can implement multiple interfaces, just like regular objects.


---

**Question 5: B) `const val` is a compile-time constant; `val` is computed at runtime**

`const val` must be known at compile time; `val` can be computed at runtime.


---



```kotlin
object Config {
    const val MAX_SIZE = 100  // ✅ Compile-time constant
    val timestamp = System.currentTimeMillis()  // ✅ Runtime value
    // const val time = System.currentTimeMillis()  // ❌ Error!
}
```
