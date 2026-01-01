---
type: "THEORY"
title: "Safe Casts (as?)"
---


Cast to a type safely, returning null if the cast fails.

### Regular Cast (as)
If you try to cast an object to a type it isn't, your program will crash.

```kotlin
val obj: Any = 42
val str = obj as String // ❌ CRASH: ClassCastException
```

### Safe Cast (as?)
The safe cast operator `as?` returns `null` instead of crashing if the cast is impossible.

```kotlin
val str = obj as? String // ✅ Returns null
```

### Practical Example
Safe casts are often combined with the Elvis operator to provide a default behavior.

---



```kotlin
fun printLength(obj: Any) {
    val str = obj as? String
    println("Length: ${str?.length ?: "Not a string"}")
}

fun main() {
    printLength("Hello")     // Length: 5
    printLength(42)          // Length: Not a string
    printLength(listOf(1, 2)) // Length: Not a string
}
```
