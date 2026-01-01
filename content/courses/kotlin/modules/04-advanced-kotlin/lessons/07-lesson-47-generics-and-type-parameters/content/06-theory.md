---
type: "THEORY"
title: "Type Constraints"
---


Type constraints restrict which types can be used with generics:

### Upper Bound Constraints

Use `:` to specify an upper bound:


### Comparable Constraint


### Multiple Constraints with `where`

When you need multiple constraints, use `where`:


---



```kotlin
interface Drawable {
    fun draw()
}

class Shape(val name: String) : Drawable, Comparable<Shape> {
    override fun draw() {
        println("Drawing $name")
    }

    override fun compareTo(other: Shape): Int {
        return name.compareTo(other.name)
    }
}

fun <T> displayAndCompare(a: T, b: T) where T : Drawable, T : Comparable<T> {
    a.draw()
    b.draw()
    println("${if (a > b) "First" else "Second"} is greater")
}

fun main() {
    val circle = Shape("Circle")
    val square = Shape("Square")
    displayAndCompare(circle, square)
    // Drawing Circle
    // Drawing Square
    // Second is greater
}
```
