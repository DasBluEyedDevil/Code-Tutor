---
type: "EXAMPLE"
title: "Practical Examples"
---

### Example 1: Grade Book
In this example, we use a map to store student names and their grades.

```kotlin
fun main() {
    val grades = mutableMapOf(
        "Alice" to 95,
        "Bob" to 82,
        "Charlie" to 78
    )

    // Adding a new student
    grades["Diana"] = 88

    // Updating a grade
    grades["Bob"] = 85

    println("=== Class Grades ===")
    for ((name, grade) in grades) {
        println("$name: $grade")
    }

    val average = grades.values.average()
    println("\nClass Average: ${"%.2f".format(average)}")
}
```

**Output:**
```text
=== Class Grades ===
Alice: 95
Bob: 85
Charlie: 78
Diana: 88

Class Average: 86.50
```

### Example 2: Inventory System
This example demonstrates managing a store's stock using a mutable map.

```kotlin
fun main() {
    val inventory = mutableMapOf(
        "Laptop" to 15,
        "Mouse" to 50,
        "Keyboard" to 30
    )

    println("=== Store Inventory ===")
    for ((item, quantity) in inventory) {
        val status = if (quantity < 20) "(Low stock)" else "(In stock)"
        println("$item: $quantity units $status")
    }

    println("\n=== Restocking Low Items ===")
    for ((item, quantity) in inventory) {
        if (quantity < 20) {
            val newQuantity = quantity + 30
            inventory[item] = newQuantity
            println("Restocked $item: $quantity -> $newQuantity")
        }
    }

    println("\n=== Updated Inventory ===")
    println(inventory)
}
```

**Output:**
```text
=== Store Inventory ===
Laptop: 15 units (Low stock)
Mouse: 50 units (In stock)
Keyboard: 30 units (In stock)

=== Restocking Low Items ===
Restocked Laptop: 15 -> 45

=== Updated Inventory ===
{Laptop=45, Mouse=50, Keyboard=30}
```

