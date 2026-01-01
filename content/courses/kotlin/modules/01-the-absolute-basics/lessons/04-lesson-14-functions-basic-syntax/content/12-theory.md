---
type: "THEORY"
title: "Function Scope and Variables"
---


### Local Variables
Variables declared inside a function are called **local variables**. They are only visible and accessible within the curly braces `{}` of that function.

```kotlin
fun main() {
    val x = 10
    printValue()
}

fun printValue() {
    // println(x) // ❌ Error: 'x' is not defined in this scope
}
```

### Function Parameters are Read-Only
When you pass a value into a function, it is treated as a `val`. You cannot change its value inside the function.

---



```kotlin
fun modifyValue(number: Int) {
    // number = number + 1  // ❌ Error: Val cannot be reassigned
    val newNumber = number + 1  // ✅ Create new variable
    println(newNumber)
}
```
