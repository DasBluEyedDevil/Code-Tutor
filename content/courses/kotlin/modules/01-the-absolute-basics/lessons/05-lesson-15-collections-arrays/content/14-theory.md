---
type: "THEORY"
title: "Solution 3: Word Frequency Counter"
---



**Solution Code**:

```kotlin
fun main() {
    println("Enter a sentence:")
    val input = readln()
    
    // Clean words (remove punctuation and lowercase)
    val words = input.lowercase()
        .replace(Regex("[^a-z ]"), "")
        .split(" ")
        .filter { it.isNotBlank() }

    val frequency = mutableMapOf<String, Int>()
    
    for (word in words) {
        val count = frequency.getOrDefault(word, 0)
        frequency[word] = count + 1
    }

    println("\n=== Word Frequency ===")
    frequency.forEach { (word, count) -> println("$word: $count") }

    val mostCommon = frequency.maxByOrNull { it.value }
    if (mostCommon != null) {
        println("\nMost common word: '${mostCommon.key}' (appears ${mostCommon.value} times)")
    }

    println("\nTotal unique words: ${frequency.size}")
    println("Total words: ${words.size}")
}
```

**Sample Run**:
