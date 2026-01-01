---
type: "THEORY"
title: "Generic Functions"
---


Functions can also be generic:

### Basic Generic Function


### Generic Function with Type Inference


### Generic Extension Functions


---



```kotlin
fun <T> T.toSingletonList(): List<T> {
    return listOf(this)
}

fun <T> List<T>.secondOrNull(): T? {
    return if (size >= 2) this[1] else null
}

fun main() {
    println(42.toSingletonList())  // [42]
    println("Hello".toSingletonList())  // [Hello]

    println(listOf(1, 2, 3).secondOrNull())  // 2
    println(listOf("a").secondOrNull())       // null
}
```
