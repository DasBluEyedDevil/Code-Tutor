---
type: "THEORY"
title: "apply: Configure and Return Object"
---


`apply` uses `this` context and returns the object itself (great for chaining!).

### Basic Usage


### Object Initialization


### Builder Pattern


### Real-World Example: Android View Configuration


---



```kotlin
// Simulated Android view
class TextView {
    var text: String = ""
    var textSize: Float = 14f
    var textColor: String = "black"

    override fun toString() = "TextView(text=$text, size=$textSize, color=$textColor)"
}

fun createTitleView() = TextView().apply {
    text = "Welcome!"
    textSize = 24f
    textColor = "blue"
}

val view = createTitleView()
println(view)
// TextView(text=Welcome!, size=24.0, color=blue)
```
