---
type: "THEORY"
title: "Chaining Operations"
---


The real power comes from combining operations.

### Example 1: E-Commerce Analysis


### Example 2: Student Grade Analysis


---



```kotlin
data class Student(val name: String, val grades: List<Int>, val major: String)

val students = listOf(
    Student("Alice", listOf(85, 90, 92), "CS"),
    Student("Bob", listOf(78, 82, 80), "Math"),
    Student("Charlie", listOf(95, 98, 96), "CS"),
    Student("Diana", listOf(88, 85, 90), "Math"),
    Student("Eve", listOf(70, 75, 72), "CS")
)

// CS students with average > 85
val topCSStudents = students
    .filter { it.major == "CS" }
    .map { student ->
        student.name to student.grades.average()
    }
    .filter { (_, avg) -> avg > 85 }
    .sortedByDescending { (_, avg) -> avg }

println("Top CS students:")
topCSStudents.forEach { (name, avg) ->
    println("  $name: ${"%.1f".format(avg)}")
}
// Top CS students:
//   Charlie: 96.3
//   Alice: 89.0

// All grades flattened and analyzed
val allGrades = students.flatMap { it.grades }
println("Total grades: ${allGrades.size}")  // 15
println("Highest grade: ${allGrades.maxOrNull()}")  // 98
println("Average: ${"%.1f".format(allGrades.average())}")  // 84.7
```
