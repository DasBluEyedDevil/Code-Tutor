---
type: "THEORY"
title: "Solution: Shape Hierarchy"
---



---



```kotlin
import kotlin.math.sqrt

abstract class Shape(val color: String) {
    abstract fun area(): Double
    abstract fun perimeter(): Double
    abstract fun draw()

    fun displayInfo() {
        println("Color: $color")
        println("Area: ${String.format("%.2f", area())}")
        println("Perimeter: ${String.format("%.2f", perimeter())}")
    }
}

class Circle(color: String, val radius: Double) : Shape(color) {
    override fun area(): Double = Math.PI * radius * radius

    override fun perimeter(): Double = 2 * Math.PI * radius

    override fun draw() {
        println("⭕ Drawing a $color circle with radius $radius")
    }
}

class Rectangle(color: String, val width: Double, val height: Double) : Shape(color) {
    override fun area(): Double = width * height

    override fun perimeter(): Double = 2 * (width + height)

    override fun draw() {
        println("▭ Drawing a $color rectangle ${width}x${height}")
    }
}

class Triangle(color: String, val side1: Double, val side2: Double, val side3: Double) : Shape(color) {

    init {
        require(isValid()) { "Invalid triangle: sides don't satisfy triangle inequality" }
    }

    private fun isValid(): Boolean {
        return side1 + side2 > side3 && side1 + side3 > side2 && side2 + side3 > side1
    }

    override fun area(): Double {
        // Heron's formula
        val s = perimeter() / 2
        return sqrt(s * (s - side1) * (s - side2) * (s - side3))
    }

    override fun perimeter(): Double = side1 + side2 + side3

    override fun draw() {
        println("△ Drawing a $color triangle with sides $side1, $side2, $side3")
    }
}

fun printTotalArea(shapes: List<Shape>) {
    val total = shapes.sumOf { it.area() }
    println("Total area of all shapes: ${String.format("%.2f", total)}")
}

fun main() {
    val shapes: List<Shape> = listOf(
        Circle("Red", 5.0),
        Rectangle("Blue", 4.0, 6.0),
        Triangle("Green", 3.0, 4.0, 5.0),
        Circle("Yellow", 3.0),
        Rectangle("Purple", 10.0, 2.0)
    )

    shapes.forEach { shape ->
        shape.draw()
        shape.displayInfo()
        println()
    }

    printTotalArea(shapes)
}
```
