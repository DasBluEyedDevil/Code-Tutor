---
type: "THEORY"
title: "Solution 1: Student Grade Manager"
---



**Solution Code**:

```kotlin
fun main() {
    val grades = mutableMapOf(
        "Alice" to 92,
        "Bob" to 78,
        "Carol" to 95,
        "Dave" to 88,
        "Eve" to 73
    )

    println("=== Student Grade Manager ===\n")

    println("All Students:")
    grades.forEach { (name, grade) -> println("  $name: $grade") }

    val average = grades.values.average()
    println("\nAverage Grade: %.2f".format(average))

    println("\nStudents with grade > 80:")
    grades.filter { it.value > 80 }
          .forEach { (name, grade) -> println("  $name: $grade") }

    val highest = grades.maxBy { it.value }
    val lowest = grades.minBy { it.value }

    println("\nHighest Grade: ${highest.key} with ${highest.value}")
    println("Lowest Grade: ${lowest.key} with ${lowest.value}")
}
```

**Sample Output**:
