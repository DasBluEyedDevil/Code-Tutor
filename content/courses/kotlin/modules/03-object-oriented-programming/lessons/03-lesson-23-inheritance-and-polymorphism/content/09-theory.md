---
type: "THEORY"
title: "Type Checking and Casting"
---


### Type Checking with `is`


### Smart Casting

Kotlin automatically casts after type checking:


### Explicit Casting


---



```kotlin
val animal: Animal = Dog("Buddy")

// Safe cast (returns null if cast fails)
val dog: Dog? = animal as? Dog
dog?.fetch()

// Unsafe cast (throws exception if cast fails)
val dog2: Dog = animal as Dog
dog2.fetch()
```
