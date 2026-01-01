---
type: "THEORY"
title: "Solution 2: String Processor with Null Safety"
---



**Solution Code**:

```kotlin
fun safeLength(str: String?) = str?.length ?: 0

fun safeUppercase(str: String?) = str?.uppercase() ?: "EMPTY"

fun extractFirstWord(str: String?) = str?.trim()?.split(" ")?.firstOrNull()

fun processText(text: String?) {
    println("=== Text Processing ===")
    println("Input: ${text ?: "null"}")
    println("Length: ${safeLength(text)}")
    println("Uppercase: ${safeUppercase(text)}")
    println("First word: ${extractFirstWord(text) ?: "none"}")
    println()
}

fun main() {
    processText("Hello World from Kotlin")
    processText("   Kotlin   ")
    processText("")
    processText(null)
}
```

**Sample Output**:
