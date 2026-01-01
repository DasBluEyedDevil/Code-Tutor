---
type: "THEORY"
title: "Enum Classes"
---


**Enum classes** define a fixed set of constants.


**Enum vs Sealed Class**:

| Feature | Enum | Sealed Class |
|---------|------|--------------|
| Fixed set of instances | ✅ Yes (all at compile-time) | ✅ Yes (types known at compile-time) |
| Can have different data | ❌ No (same structure) | ✅ Yes (different properties) |
| Can inherit | ❌ No | ✅ Yes |
| When to use | Finite set of constants | Type hierarchies with different data |

---



```kotlin
enum class Direction {
    NORTH, SOUTH, EAST, WEST
}

enum class Priority(val level: Int) {
    LOW(1),
    MEDIUM(2),
    HIGH(3),
    CRITICAL(4);

    fun isUrgent() = level >= 3
}

fun main() {
    val direction = Direction.NORTH
    println(direction)  // NORTH

    val priority = Priority.HIGH
    println("Level: ${priority.level}")  // Level: 3
    println("Urgent: ${priority.isUrgent()}")  // Urgent: true

    // Iterate over all values
    Priority.values().forEach { p ->
        println("${p.name}: Level ${p.level}")
    }

    // String to enum
    val p = Priority.valueOf("MEDIUM")
    println(p.level)  // 2
}
```
