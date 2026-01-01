---
type: "THEORY"
title: "Return Values: Getting Data Back"
---


### Basic Return
A function can "return" a result back to where it was called using the `return` keyword.

```kotlin
fun add(a: Int, b: Int): Int {
    return a + b
}

fun main() {
    val result = add(5, 10)
    println("The result is $result")
}
```

**Return Type Syntax**: The return type follows the parentheses: `(): Int`.

### Multiple Return Statements
A function can have multiple `return` statements, often inside an `if` block.

```kotlin
fun getGrade(score: Int): String {
    if (score >= 90) return "A"
    if (score >= 80) return "B"
    return "C"
}
```

### Unit Return Type (No Return Value)
...



```kotlin
// These are equivalent:
fun sayHello(): Unit {
    println("Hello!")
}

fun sayGoodbye() {  // Unit is implicit if omitted
    println("Goodbye!")
}
```
