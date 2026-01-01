---
type: "EXAMPLE"
title: "Improved Type Inference"
---


K2 has smarter type inference that handles complex cases better:



```kotlin
// K2 handles builder patterns better

// Before K2 (K1 - sometimes failed):
// val result = buildList {
//     add("string")
//     add(if (condition) 1 else "fallback")  // Type error in K1
// }

// With K2 (works correctly):
val condition = true
val result = buildList {
    add("string")
    if (condition) add(1) else add("fallback")  // OK, infers List<Any>
}

// K2 infers types through complex expressions
val data = mapOf(
    "users" to listOf(
        mapOf("name" to "Alice", "age" to 30),
        mapOf("name" to "Bob", "age" to 25)
    )
)

// K2 correctly infers: Map<String, List<Map<String, Any>>>
val users = data["users"]
val firstUser = users?.firstOrNull()

// K2 also handles when expressions better
val value: Any = "test"
val length = when (value) {
    is String -> value.length  // Smart cast works
    is List<*> -> value.size   // Smart cast works
    else -> 0
}
```
