---
type: "THEORY"
title: "Modifying Mutable Lists"
---


### Adding Elements


### Removing Elements


### Updating Elements


---



```kotlin
fun main() {
    val tasks = mutableListOf("Buy milk", "Call mom", "Study Kotlin")

    // Update by index
    tasks[0] = "Buy groceries"
    println(tasks)  // [Buy groceries, Call mom, Study Kotlin]

    // Update with set()
    tasks.set(1, "Video call mom")
    println(tasks)  // [Buy groceries, Video call mom, Study Kotlin]
}
```
