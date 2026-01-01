---
type: "THEORY"
title: "Overriding Methods"
---


To override a method from the superclass:
1. The superclass method must be marked `open`
2. Use the `override` keyword in the subclass


---



```kotlin
open class Shape {
    open fun draw() {
        println("Drawing a shape")
    }

    open fun area(): Double {
        return 0.0
    }
}

class Circle(val radius: Double) : Shape() {
    override fun draw() {
        println("Drawing a circle with radius $radius")
    }

    override fun area(): Double {
        return Math.PI * radius * radius
    }
}

class Rectangle(val width: Double, val height: Double) : Shape() {
    override fun draw() {
        println("Drawing a rectangle $width x $height")
    }

    override fun area(): Double {
        return width * height
    }
}

fun main() {
    val circle = Circle(5.0)
    circle.draw()  // Drawing a circle with radius 5.0
    println("Area: ${circle.area()}")  // Area: 78.53981633974483

    val rect = Rectangle(4.0, 6.0)
    rect.draw()  // Drawing a rectangle 4.0 x 6.0
    println("Area: ${rect.area()}")  // Area: 24.0
}
```
