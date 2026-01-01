---
type: "ANALOGY"
title: "The Concept"
---


### Properties vs Fields

In many languages (like Java), classes have **fields** (private variables) and **getter/setter methods** to access them:

**Java (verbose)**:

**Kotlin (clean)**:

In Kotlin, properties automatically have getters (and setters for `var`). You access them like fields, but they're actually calling methods behind the scenes!


---



```kotlin
val person = Person()
person.name = "Alice"  // Calls setter
println(person.name)    // Calls getter
```
