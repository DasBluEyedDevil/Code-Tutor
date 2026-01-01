---
type: "EXAMPLE"
title: "Chaining Operations"
---


Build pipelines that short-circuit on errors:



```kotlin
fun parseNumber(s: String): Result<Int> = runCatching { s.toInt() }

fun divide(a: Int, b: Int): Result<Double> = runCatching {
    require(b != 0) { "Cannot divide by zero" }
    a.toDouble() / b
}

// Chain operations - any failure stops the chain
fun calculate(input: String): Result<String> =
    parseNumber(input)
        .mapCatching { it * 2 }
        .mapCatching { divide(it, 3).getOrThrow() }
        .map { "Result: $it" }

println(calculate("15").getOrNull())   // "Result: 10.0"
println(calculate("abc").isFailure)    // true (parse failed)
println(calculate("0").getOrNull())    // "Result: 0.0"

// Recovering from errors mid-chain
fun calculateWithRecovery(input: String): Result<String> =
    parseNumber(input)
        .recover { 0 }  // Recover parse errors with default
        .mapCatching { it * 2 }
        .map { "Result: $it" }
```
