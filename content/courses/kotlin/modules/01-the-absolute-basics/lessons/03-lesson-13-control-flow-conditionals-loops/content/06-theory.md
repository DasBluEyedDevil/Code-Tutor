---
type: "THEORY"
title: "Loops"
---


### For Loop with Ranges
The `for` loop is used to iterate over anything that provides an iterator, like a range of numbers.

```kotlin
for (i in 1..5) {
    println("Count: $i") // Prints 1, 2, 3, 4, 5
}
```

### While Loop
The `while` loop repeats a block of code as long as its condition is `true`.

```kotlin
var counter = 5
while (counter > 0) {
    println(counter)
    counter--
}
```

### Do-While Loop
The `do-while` loop is similar to `while`, but it checks the condition *after* executing the block. This means the code inside **runs at least once**.

```kotlin
var x = 10
do {
    println("X is $x")
    x++
} while (x < 10) // Prints "X is 10" even though 10 is not < 10
```

### Break and Continue

These keywords give you finer control over loop execution:

- **`break`**: Immediately exits the loop entirely
- **`continue`**: Skips the rest of the current iteration and moves to the next one

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
