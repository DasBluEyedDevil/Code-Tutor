---
type: "THEORY"
title: "Exercise 2: Null Safety with let"
---


**Goal**: Use `let` for safe null handling.

**Task**: Process nullable user input safely:


---



```kotlin
fun processUserInput(input: String?): String {
    // TODO: Use let to safely process input
    // 1. Trim whitespace
    // 2. Convert to uppercase
    // 3. Return processed string or "NO INPUT" if null/empty
}

fun main() {
    println(processUserInput("  hello  "))  // Should print: HELLO
    println(processUserInput(null))         // Should print: NO INPUT
    println(processUserInput("   "))        // Should print: NO INPUT
}
```
