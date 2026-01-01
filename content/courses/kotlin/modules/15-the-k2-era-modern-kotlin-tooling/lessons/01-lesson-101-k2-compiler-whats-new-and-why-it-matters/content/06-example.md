---
type: "EXAMPLE"
title: "Smarter Smart Casts"
---


K2 tracks types through more complex control flow:



```kotlin
// K2 handles compound conditions better
fun process(value: Any) {
    // K2: Smart cast works through && conditions
    if (value is String && value.length > 5) {
        // value is smart-cast to String in BOTH parts of the condition
        println(value.uppercase())  // Works!
    }

    // K2 handles when expressions with complex conditions
    when {
        value is List<*> && value.isNotEmpty() -> {
            // value is List<*> here
            println("First element: ${value.first()}")
        }
        value is Map<*, *> && value.containsKey("id") -> {
            // value is Map<*, *> here
            println("Has ID: ${value["id"]}")
        }
    }
}

// K2 tracks through variable assignments
fun processNullable(input: String?) {
    val value = input
    if (value != null) {
        // K2 knows both 'value' and 'input' are non-null here
        println(value.length)
        println(input.length)  // Also works in K2!
    }
}

// K2 handles inline function contracts better
fun example(list: List<Any>) {
    val strings = list.filterIsInstance<String>()
    // K2 correctly infers List<String>
    strings.forEach { println(it.uppercase()) }
}
```
