---
type: "EXAMPLE"
title: "Smarter Smart Casts"
---

K2 compiler's smart casts work in more scenarios. They now handle complex conditions, work across property accesses, and track nullability through when expressions - reducing the need for explicit casts.

```kotlin
// Kotlin 2.0 has smarter smart casts

// Example 1: Smart casts work across more scenarios
fun processValue(value: Any) {
    if (value is String && value.isNotEmpty()) {
        // K2 remembers 'value' is a non-empty String
        println("String length: ${value.length}")
    }
}

// Example 2: Smart casts with when expressions
fun describe(obj: Any): String = when {
    obj is String -> "String of length ${obj.length}"  // Smart cast to String
    obj is Int && obj > 0 -> "Positive number: $obj"   // Smart cast to Int
    obj is List<*> -> "List with ${obj.size} elements" // Smart cast to List
    else -> "Unknown type"
}

// Example 3: Smart casts in complex conditions
class Container(val item: Any?)

fun processContainer(container: Container) {
    if (container.item != null && container.item is String) {
        // K2 smart casts container.item to String
        println("Item: ${container.item.uppercase()}")
    }
}

fun main() {
    processValue("Hello Kotlin 2.0")
    println(describe("test"))
    println(describe(42))
    println(describe(listOf(1, 2, 3)))
    processContainer(Container("kotlin"))
}
```
