---
type: "THEORY"
title: "The OR Operator (||)"
---


The OR operator (`||`) returns `true` when **AT LEAST ONE** condition is true.

### Truth Table for OR

| Condition A | Condition B | A \|\| B |
|-------------|-------------|----------|
| true | true | **true** |
| true | false | **true** |
| false | true | **true** |
| false | false | false |

**Think of it as:** "This **OR** that" - you need **at least one**.

### Basic OR Example


**Output:**

Even though `isPremiumMember` is false, `hasVIPPass` is true, so the condition succeeds!

### Real-World OR Examples

**Example 1: Weekend check**

**Example 2: Discount eligibility**

**Output:**

The person is over 65, so they qualify (even though they're not a student).

**Example 3: Emergency access**

---



```kotlin
fun main() {
    val isAdmin = false
    val isEmergency = true

    if (isAdmin || isEmergency) {
        println("Access granted")
    } else {
        println("Access denied")
    }
}
```
