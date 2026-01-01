---
type: "ANALOGY"
title: "The Concept: When as a Switchboard"
---


### Real-World Analogy: The Hotel Concierge

Imagine a hotel concierge helping guests:


The concierge efficiently routes to one answer based on the weather. That's exactly what `when` does—it evaluates an expression once and routes to the matching branch.

### The if-else-if Problem

Let's see why we need `when`. Here's a day-of-week converter using if-else:


This works, but it's:
- **Repetitive**: `dayNumber ==` appears 7 times
- **Verbose**: 19 lines for a simple mapping
- **Error-prone**: Easy to make mistakes in long chains

**The same logic with `when`:**


Only 10 lines! Clean, readable, and elegant.

---



```kotlin
val dayNumber = 3
val dayName = when (dayNumber) {
    1 -> "Monday"
    2 -> "Tuesday"
    3 -> "Wednesday"
    4 -> "Thursday"
    5 -> "Friday"
    6 -> "Saturday"
    7 -> "Sunday"
    else -> "Invalid day"
}
```
