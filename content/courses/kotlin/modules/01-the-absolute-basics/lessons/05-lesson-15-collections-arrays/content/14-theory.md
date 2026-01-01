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

```text
Enter a sentence:
The quick brown fox jumps over the lazy dog the fox is quick

=== Word Frequency ===
the: 3
quick: 2
brown: 1
fox: 2
jumps: 1
over: 1
lazy: 1
dog: 1
is: 1

Most common word: 'the' (appears 3 times)

Total unique words: 9
Total words: 12
```
