---
type: "THEORY"
title: "Nullable vs Non-Nullable Types"
---


### Non-Nullable Types (Default)
In Kotlin, you cannot assign `null` to a regular variable. This guarantees that your program won't crash when you use that variable.

```kotlin
var name: String = "Alice"
// name = null ❌ Error: Null can not be a value of a non-null type String
```

### Nullable Types (Type?)
If you *want* a variable to be able to hold a `null` value, you must explicitly declare it by adding a question mark `?` after the type.

```kotlin
var middleName: String? = "John"
middleName = null // ✅ OK
```

---



```kotlin
val age: Int = 25       // Cannot be null
val age: Int? = null    // Can be null

val price: Double = 19.99  // Cannot be null
val price: Double? = null  // Can be null

val isActive: Boolean = true  // Cannot be null
val isActive: Boolean? = null // Can be null
```
