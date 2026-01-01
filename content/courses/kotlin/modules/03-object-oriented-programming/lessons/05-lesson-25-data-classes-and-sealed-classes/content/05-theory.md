---
type: "THEORY"
title: "Destructuring Declarations"
---


Data classes support **destructuring** - extracting multiple values at once:


**How it works**: Kotlin generates `component1()`, `component2()`, etc. functions:


**Partial Destructuring**:


**Destructuring in Loops**:


---



```kotlin
data class Person(val name: String, val age: Int)

val people = listOf(
    Person("Alice", 25),
    Person("Bob", 30),
    Person("Carol", 22)
)

for ((name, age) in people) {
    println("$name is $age years old")
}
```
