---
type: "THEORY"
title: "Safe Casts (as?)"
---


Cast to a type safely, returning null if the cast fails.

### Regular Cast (as)


### Safe Cast (as?)


### Practical Example


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
