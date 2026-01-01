---
type: "EXAMPLE"
title: "Transforming Tracks"
---


Operations that stay on or switch tracks:



```kotlin
import arrow.core.*

// map - transform success value (stays on success track)
val doubled: Either<String, Int> = 21.right().map { it * 2 }  // Right(42)

// mapLeft - transform error value (stays on failure track)
val translated: Either<String, Int> = "error".left<String, Int>()
    .mapLeft { it.uppercase() }  // Left("ERROR")

// bimap - transform both tracks
val both: Either<String, Int> = 21.right<String, Int>()
    .bimap(
        leftOperation = { it.uppercase() },
        rightOperation = { it * 2 }
    )  // Right(42)

// recover - switch from failure to success
fun parseNumber(s: String): Either<String, Int> =
    Either.catch { s.toInt() }
        .mapLeft { "Not a number: $s" }

val recovered: Either<String, Int> = parseNumber("abc")
    .recover { 0 }  // Right(0)

// handleErrorWith - switch from failure with another Either
fun parseWithFallback(s: String): Either<String, Int> =
    parseNumber(s)
        .handleErrorWith { error ->
            parseNumber(s.trim())  // Try again with trimmed input
        }
```
