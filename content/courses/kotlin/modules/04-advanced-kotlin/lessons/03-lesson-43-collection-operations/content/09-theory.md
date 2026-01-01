---
type: "THEORY"
title: "FlatMap and Flatten"
---


### flatten: Flatten Nested Collections


### flatMap: Map Then Flatten


### Practical Example: Hierarchical Data


---



```kotlin
data class Department(val name: String, val employees: List<Employee>)
data class Employee(val name: String, val skills: List<String>)

val departments = listOf(
    Department("Engineering", listOf(
        Employee("Alice", listOf("Kotlin", "Java", "Python")),
        Employee("Bob", listOf("JavaScript", "TypeScript"))
    )),
    Department("Design", listOf(
        Employee("Charlie", listOf("Figma", "Photoshop")),
        Employee("Diana", listOf("Illustrator", "Sketch"))
    ))
)

// All employees across departments
val allEmployees = departments.flatMap { it.employees }
println("Total employees: ${allEmployees.size}")  // 4

// All unique skills across company
val allSkills = departments
    .flatMap { it.employees }
    .flatMap { it.skills }
    .toSet()
println("All skills: $allSkills")
// [Kotlin, Java, Python, JavaScript, TypeScript, Figma, Photoshop, Illustrator, Sketch]
```
