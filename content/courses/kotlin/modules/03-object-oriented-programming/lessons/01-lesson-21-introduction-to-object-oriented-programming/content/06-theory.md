---
type: "THEORY"
title: "Constructors"
---


### Primary Constructor

A **constructor** is a special function that initializes an object when it's created. The **primary constructor** is defined in the class header.

**Without Constructor** (tedious):


**With Constructor** (clean):


**Explanation**:
- `class Person(val name: String, val age: Int)` defines properties in the constructor
- `val` or `var` makes them properties (accessible throughout the class)
- Without `val`/`var`, they're just constructor parameters

**Constructor with Default Values**:


### Init Block

The **`init` block** runs when an object is created. Use it for validation or setup logic.


### Secondary Constructors

**Secondary constructors** provide alternative ways to create objects.


**Note**: In modern Kotlin, **default parameters** are preferred over secondary constructors.

---



```kotlin
class Person(val name: String, val age: Int) {
    var email: String = ""

    // Secondary constructor
    constructor(name: String, age: Int, email: String) : this(name, age) {
        this.email = email
    }

    fun displayInfo() {
        println("Name: $name, Age: $age, Email: $email")
    }
}

fun main() {
    val person1 = Person("Alice", 25)
    person1.displayInfo()  // Name: Alice, Age: 25, Email:

    val person2 = Person("Bob", 30, "bob@example.com")
    person2.displayInfo()  // Name: Bob, Age: 30, Email: bob@example.com
}
```
