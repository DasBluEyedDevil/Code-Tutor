---
type: "EXAMPLE"
title: "Real-World Data Class Examples"
---


### Example 1: API Response


### Example 2: Coordinates and Geometry


---



```kotlin
data class Point(val x: Double, val y: Double) {
    fun distanceTo(other: Point): Double {
        val dx = x - other.x
        val dy = y - other.y
        return kotlin.math.sqrt(dx * dx + dy * dy)
    }
}

data class Rectangle(val topLeft: Point, val bottomRight: Point) {
    val width: Double
        get() = bottomRight.x - topLeft.x

    val height: Double
        get() = bottomRight.y - topLeft.y

    val area: Double
        get() = width * height
}

fun main() {
    val p1 = Point(0.0, 0.0)
    val p2 = Point(3.0, 4.0)

    println("Distance: ${p1.distanceTo(p2)}")  // 5.0

    val rect = Rectangle(Point(0.0, 10.0), Point(5.0, 0.0))
    println("Area: ${rect.area}")  // 50.0
}
```
