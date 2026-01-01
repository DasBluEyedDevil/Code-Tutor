---
type: "THEORY"
title: "Operator Overloading"
---


Define how operators work with custom types.

### Arithmetic Operators


### Comparison Operators


### Invoke Operator (Callable Objects)


### Index Access Operator


---



```kotlin
class Grid(val width: Int, val height: Int) {
    private val data = Array(width * height) { 0 }

    operator fun get(x: Int, y: Int): Int {
        return data[y * width + x]
    }

    operator fun set(x: Int, y: Int, value: Int) {
        data[y * width + x] = value
    }
}

val grid = Grid(3, 3)
grid[1, 2] = 42
println(grid[1, 2])  // 42
```
