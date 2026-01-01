---
type: "THEORY"
title: "Basic For Loop with Ranges"
---


### Your First For Loop

```kotlin
for (i in 1..5) {
    println("Count: $i")
}
```

**Output:**
```text
Count: 1
Count: 2
Count: 3
Count: 4
Count: 5
```

**How it works:**
1. `for` - Keyword that starts the loop
2. `i` - Loop variable (can be any name)
3. `in` - Keyword meaning "within" or "through"
4. `1..5` - Range from 1 to 5 (inclusive)
5. Loop body executes once for each value in the range

### Anatomy of a For Loop

```kotlin
for (variable in collection) {
    // Code to repeat for each item
}
```

**Visual flow:**
```text
Start → Is there a next item? → Yes → Execute loop body → Go back to check
                              ↓ No
                           End loop
```

### Practical Example: Countdown Timer

```kotlin
fun main() {
    println("Rocket launch countdown:")
    for (count in 10 downTo 1) {
        println("$count...")
        Thread.sleep(1000) // Wait 1 second
    }
    println("🚀 BLAST OFF!")
}
```

**Output:**
```text
Rocket launch countdown:
10...
9...
8...
7...
6...
5...
4...
3...
2...
1...
🚀 BLAST OFF!
```

---
