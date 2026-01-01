---
type: "THEORY"
title: "Exercise 2: String Processor with Null Safety"
---


**Goal**: Create safe string processing functions.

**Starter Code**:
```kotlin
fun safeLength(str: String?): Int = TODO()
fun safeUppercase(str: String?): String = TODO()

fun main() {
    processText("Hello World")
    processText(null)
}
```

**Expected Output**:
```text
=== Text Processing ===
Input: Hello World
Length: 11
Uppercase: HELLO WORLD
...
```


