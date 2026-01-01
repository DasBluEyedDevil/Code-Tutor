---
type: "THEORY"
title: "Solution 2: Rectangle Calculator"
---

This solution demonstrates reading user input, performing calculations with variables, and displaying formatted output.

```kotlin
fun main() {
    println("=== Rectangle Calculator ===")

    println("Enter width (meters):")
    val width = readln().toDouble()

    println("Enter height (meters):")
    val height = readln().toDouble()

    val area = width * height
    val perimeter = 2 * (width + height)

    println("\nResults:")
    println("Area: $area square meters")
    println("Perimeter: $perimeter meters")
}
```
