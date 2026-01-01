---
type: "EXAMPLE"
title: "Recovering from Errors"
---


Transform failures back to successes:



```kotlin
fun parseNumber(s: String): Result<Int> = runCatching { s.toInt() }

// recover - transform failure to success
val recovered: Result<Int> = parseNumber("abc")
    .recover { 0 }  // On any error, use 0

println(recovered.getOrThrow())  // 0

// recoverCatching - recover but might fail again
val partialRecover: Result<Int> = parseNumber("abc")
    .recoverCatching { error ->
        // Try parsing as hex
        error.message?.let { Integer.parseInt(it, 16) }
            ?: throw error
    }

// Selective recovery
fun parseWithDefault(s: String, default: Int): Result<Int> =
    parseNumber(s).recover { error ->
        when (error) {
            is NumberFormatException -> default
            else -> throw error  // Re-throw other errors
        }
    }
```
