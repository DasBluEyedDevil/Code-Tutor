---
type: "THEORY"
title: "What's Next?"
---


For loops are great when you know how many times to iterate, but what about situations where you need to repeat until a condition is met? What if you need to keep asking for valid input until the user gets it right?

In **Lesson 2.5: While Loops and Do-While**, you'll learn:
- While loops for condition-based repetition
- Do-while loops (execute at least once)
- Break and continue for loop control
- Infinite loops and how to guard against them

**Preview:**

---

**Fantastic progress! You've completed Lesson 2.4. Keep up the momentum!** 🎉



```kotlin
var attempts = 0
while (attempts < 3) {
    println("Attempt ${attempts + 1}")
    attempts++
}

do {
    val input = readln()
} while (input != "quit")
```
