---
type: "THEORY"
title: "Nullable vs Non-Nullable Types"
---


### Non-Nullable Types (Default)


**By default, all types in Kotlin are non-nullable.**

### Nullable Types (Type?)

Add `?` to make a type nullable:


**Examples**:

---



```kotlin
val age: Int = 25       // Cannot be null
val age: Int? = null    // Can be null

val price: Double = 19.99  // Cannot be null
val price: Double? = null  // Can be null

val isActive: Boolean = true  // Cannot be null
val isActive: Boolean? = null // Can be null
```
