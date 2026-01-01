---
type: "THEORY"
title: "Classes and Objects"
---


### What is a Class?

A **class** is a blueprint or template for creating objects. It defines:
- **Properties**: What data the object holds
- **Methods**: What actions the object can perform

**Analogy**: A class is like a cookie cutter, and objects are the cookies.


### Creating Your First Class

**Syntax**:


**Example: Person Class**


**Key Points**:
- `class Person` defines the blueprint
- `Person()` creates a new instance (object)
- Each object has its own independent data
- `person1` and `person2` are separate objects

---



```kotlin
class Person {
    var name: String = ""
    var age: Int = 0

    fun introduce() {
        println("Hi, I'm $name and I'm $age years old.")
    }
}

fun main() {
    // Create an object (instance) of Person
    val person1 = Person()
    person1.name = "Alice"
    person1.age = 25
    person1.introduce()  // Hi, I'm Alice and I'm 25 years old.

    // Create another object
    val person2 = Person()
    person2.name = "Bob"
    person2.age = 30
    person2.introduce()  // Hi, I'm Bob and I'm 30 years old.
}
```
