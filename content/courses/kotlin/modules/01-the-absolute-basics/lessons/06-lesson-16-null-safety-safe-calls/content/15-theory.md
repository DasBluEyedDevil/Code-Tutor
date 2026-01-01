---
type: "THEORY"
title: "Exercise 2: String Processor with Null Safety"
---


**Goal**: Create safe string processing functions.

**Starter Code**:
```kotlin
fun safeLength(str: String?): Int {
    // Return the length of the string, or 0 if null
    // Hint: Use the Elvis operator
}

fun safeUppercase(str: String?): String {
    // Return the uppercase version, or "N/A" if null
    // Hint: Use safe call with Elvis
}

fun processText(text: String?) {
    // Print formatted output using the above functions
}

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

=== Text Processing ===
Input: null
Length: 0
Uppercase: N/A
```


