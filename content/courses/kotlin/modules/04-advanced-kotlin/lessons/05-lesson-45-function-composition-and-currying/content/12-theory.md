---
type: "THEORY"
title: "Solution 1: Function Composition"
---



**Explanation**:
- `andThen`: Read left-to-right (intuitive)
- `compose`: Mathematical notation (right-to-left)
- Both achieve the same result, different reading order

---



```kotlin
infix fun <A, B, C> ((A) -> B).andThen(other: (B) -> C): (A) -> C {
    return { x -> other(this(x)) }
}

infix fun <A, B, C> ((B) -> C).compose(other: (A) -> B): (A) -> C {
    return { x -> this(other(x)) }
}

fun main() {
    val trim: (String) -> String = { it.trim() }
    val uppercase: (String) -> String = { it.uppercase() }
    val addExclamation: (String) -> String = { "$it!" }

    // andThen: left to right
    val process1 = trim andThen uppercase andThen addExclamation
    println(process1("  hello  "))  // HELLO!

    // compose: right to left
    val process2 = addExclamation compose uppercase compose trim
    println(process2("  world  "))  // WORLD!

    // Practical example: data processing
    val validate: (String) -> String? = { if (it.isNotEmpty()) it else null }
    val normalize: (String) -> String = { it.trim().lowercase() }
    val hash: (String) -> Int = { it.hashCode() }

    val pipeline = normalize andThen hash
    println("Hash: ${pipeline("  HELLO  ")}")  // Hash of "hello"
}
```
