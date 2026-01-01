---
type: "ANALOGY"
title: "The Concept: Metadata and Introspection"
---


### Why Annotations?

Annotations attach metadata to code elements:


### Why Reflection?

Reflection lets you inspect code structure at runtime:


---



```kotlin
data class User(val name: String, val age: Int)

fun main() {
    val user = User("Alice", 25)
    val kClass = user::class

    println("Class: ${kClass.simpleName}")
    println("Properties:")
    kClass.memberProperties.forEach { prop ->
        println("  ${prop.name} = ${prop.get(user)}")
    }
}
// Output:
// Class: User
// Properties:
//   age = 25
//   name = Alice
```
