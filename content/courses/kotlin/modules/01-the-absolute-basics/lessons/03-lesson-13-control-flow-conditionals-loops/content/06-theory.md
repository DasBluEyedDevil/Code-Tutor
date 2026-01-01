---
type: "THEORY"
title: "Loops"
---


### For Loop with Ranges


### While Loop


### Do-While Loop

Runs at least once:


### Break and Continue


---



```kotlin
// Break - exit loop early
for (i in 1..10) {
    if (i == 5) break
    println(i)  // 1, 2, 3, 4
}

// Continue - skip current iteration
for (i in 1..10) {
    if (i % 2 == 0) continue  // Skip even numbers
    println(i)  // 1, 3, 5, 7, 9
}
```
