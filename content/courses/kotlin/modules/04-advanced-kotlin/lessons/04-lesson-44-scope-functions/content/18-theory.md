---
type: "THEORY"
title: "Solution 2: Null Safety with let"
---



**Explanation**:
- `?.` safe call operator works with `let`
- `takeIf` filters out empty strings
- `let` chains transformations safely
- Elvis operator (`?:`) provides default

---



```kotlin
fun processUserInput(input: String?): String {
    return input
        ?.trim()
        ?.takeIf { it.isNotEmpty() }
        ?.let { it.uppercase() }
        ?: "NO INPUT"
}

// Alternative with more explicit let
fun processUserInputAlt(input: String?): String {
    return input?.let { rawInput ->
        rawInput.trim()
    }?.let { trimmed ->
        trimmed.takeIf { it.isNotEmpty() }
    }?.let { validated ->
        validated.uppercase()
    } ?: "NO INPUT"
}

fun main() {
    println(processUserInput("  hello  "))  // HELLO
    println(processUserInput(null))         // NO INPUT
    println(processUserInput("   "))        // NO INPUT

    println("\nAlternative version:")
    println(processUserInputAlt("  world  "))  // WORLD
    println(processUserInputAlt(null))         // NO INPUT
}
```
