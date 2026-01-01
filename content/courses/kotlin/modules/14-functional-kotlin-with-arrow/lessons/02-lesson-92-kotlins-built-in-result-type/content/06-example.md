---
type: "EXAMPLE"
title: "Transforming Results with map"
---


Transform success values while preserving errors:



```kotlin
fun parseNumber(s: String): Result<Int> = runCatching { s.toInt() }

// map - transform success value (failure passes through)
val doubled: Result<Int> = parseNumber("21").map { it * 2 }
println(doubled.getOrNull())  // 42

val failed: Result<Int> = parseNumber("abc").map { it * 2 }
println(failed.isFailure)  // true - error preserved

// mapCatching - transform with potential failure
fun parseAndDouble(s: String): Result<Int> =
    parseNumber(s)
        .mapCatching { number ->
            require(number > 0) { "Must be positive" }
            number * 2
        }

println(parseAndDouble("5").getOrNull())   // 10
println(parseAndDouble("-5").isFailure)    // true
println(parseAndDouble("abc").isFailure)   // true
```
