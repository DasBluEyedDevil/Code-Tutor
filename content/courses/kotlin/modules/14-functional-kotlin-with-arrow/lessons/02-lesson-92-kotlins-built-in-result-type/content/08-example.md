---
type: "EXAMPLE"
title: "Handling Results with fold"
---


Process both success and failure cases:



```kotlin
fun processInput(input: String): String {
    val result = parseNumber(input)

    // fold - handle both cases, return single value
    return result.fold(
        onSuccess = { number -> "Parsed: $number" },
        onFailure = { error -> "Error: ${error.message}" }
    )
}

println(processInput("42"))   // "Parsed: 42"
println(processInput("abc"))  // "Error: For input string: \"abc\""

// Side effects with onSuccess/onFailure
fun logResult(result: Result<Int>) {
    result
        .onSuccess { println("Got value: $it") }
        .onFailure { println("Got error: ${it.message}") }
}

// Using when for pattern matching
fun handleResult(result: Result<Int>): String = when {
    result.isSuccess -> "Value: ${result.getOrThrow()}"
    else -> "Error: ${result.exceptionOrNull()?.message}"
}
```
