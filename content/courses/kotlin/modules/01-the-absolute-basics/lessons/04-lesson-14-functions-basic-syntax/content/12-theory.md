---
type: "THEORY"
title: "Function Scope and Variables"
---


### Local Variables

Variables inside functions are **local**—they only exist within that function:


### Function Parameters are Read-Only


---



```kotlin
fun modifyValue(number: Int) {
    // number = number + 1  // ❌ Error: Val cannot be reassigned
    val newNumber = number + 1  // ✅ Create new variable
    println(newNumber)
}
```
