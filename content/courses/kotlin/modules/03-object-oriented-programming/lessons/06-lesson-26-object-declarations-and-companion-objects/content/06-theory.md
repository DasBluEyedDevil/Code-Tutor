---
type: "THEORY"
title: "Companion Objects"
---


**Companion objects** are object declarations inside a class, providing "static-like" members.

### Basic Companion Object


**Output**:

### Factory Methods

Companion objects are perfect for factory methods:


### Named Companion Objects


### Companion Object Implementing Interface


---



```kotlin
interface JsonSerializer {
    fun toJson(obj: Any): String
}

class User(val name: String, val age: Int) {
    companion object : JsonSerializer {
        override fun toJson(obj: Any): String {
            if (obj !is User) return "{}"
            return """{"name": "${obj.name}", "age": ${obj.age}}"""
        }
    }
}

fun main() {
    val user = User("Alice", 25)
    val json = User.toJson(user)
    println(json)  // {"name": "Alice", "age": 25}
}
```
