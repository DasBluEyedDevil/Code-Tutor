---
type: "THEORY"
title: "Type Safety and Type Checking"
---


Kotlin is **strongly typed**—you can't mix types without converting:


**Check a variable's type**:

---



```kotlin
val number = 42

println(number is Int)     // true
println(number is String)  // false
println(number is Double)  // false
```
