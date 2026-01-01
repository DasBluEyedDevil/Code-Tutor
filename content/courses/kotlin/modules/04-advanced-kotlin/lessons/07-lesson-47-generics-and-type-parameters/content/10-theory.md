---
type: "THEORY"
title: "Reified Type Parameters"
---


Normally, type information is erased at runtime. `reified` preserves it:

### The Problem: Type Erasure


### The Solution: Reified


### Reified with Class Checking


### Reified with JSON Parsing (Practical Example)


**Requirements for `reified`**:
- Function must be `inline`
- Can use `is`, `as`, `::class` with type parameter
- Cannot be used in non-inline functions

---



```kotlin
import kotlin.reflect.KClass

// Simulated JSON parser
inline fun <reified T : Any> parseJson(json: String): T {
    println("Parsing JSON to ${T::class.simpleName}")
    // In real code, you'd use a JSON library
    return when (T::class) {
        String::class -> json as T
        Int::class -> json.toInt() as T
        else -> throw IllegalArgumentException("Unsupported type")
    }
}

fun main() {
    val str = parseJson<String>("\"Hello\"")
    val num = parseJson<Int>("42")

    println("String: $str")  // String: "Hello"
    println("Int: $num")     // Int: 42
}
```
