---
type: "THEORY"
title: "Return Values: Lambda Result vs Object"
---


### Lambda Result Functions: let, run, with


### Object Functions: apply, also


### Why It Matters for Chaining


---



```kotlin
// apply and also return object - chainable!
val person = Person("Alice", 25)
    .apply { age += 1 }
    .also { println("Created: $it") }
    .apply { name = name.uppercase() }

// let, run, with return result - chains break
val result = Person("Alice", 25)
    .run { age + 1 }  // Returns Int, can't call Person methods anymore
    // .apply { ... }  // ERROR: Int doesn't have apply with Person context
```
