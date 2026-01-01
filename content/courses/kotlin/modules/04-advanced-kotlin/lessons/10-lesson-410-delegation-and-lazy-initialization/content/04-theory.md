---
type: "THEORY"
title: "Class Delegation"
---


The `by` keyword delegates interface implementation to another object.

### Basic Class Delegation


### Multiple Interface Delegation


### Real-World Example: Window Decoration


---



```kotlin
interface Window {
    fun draw()
    fun getDescription(): String
}

class SimpleWindow : Window {
    override fun draw() {
        println("Drawing window")
    }

    override fun getDescription(): String = "Simple window"
}

class ScrollableWindow(window: Window) : Window by window {
    override fun draw() {
        window.draw()
        println("Adding scrollbars")
    }

    override fun getDescription(): String = "${window.getDescription()} with scrollbars"
}

class BorderedWindow(window: Window) : Window by window {
    override fun draw() {
        window.draw()
        println("Adding border")
    }

    override fun getDescription(): String = "${window.getDescription()} with border"
}

fun main() {
    val window = BorderedWindow(ScrollableWindow(SimpleWindow()))
    window.draw()
    println(window.getDescription())
}
// Output:
// Drawing window
// Adding scrollbars
// Adding border
// Simple window with scrollbars with border
```
