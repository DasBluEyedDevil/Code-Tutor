---
type: "THEORY"
title: "The AND Operator (&&)"
---


The AND operator (`&&`) returns `true` only when **BOTH** conditions are true.

### Truth Table for AND

| Condition A | Condition B | A && B |
|-------------|-------------|--------|
| true | true | **true** |
| true | false | false |
| false | true | false |
| false | false | false |

**Think of it as:** "This **AND** that" - you need **both**.

### Basic AND Example


**Output:**

**What if hasID was false?**

**Output:**

### Real-World AND Examples

**Example 1: Age and license check**

**Example 2: Login validation**

**Example 3: Range check (value between two numbers)**

### Chaining Multiple AND Conditions

You can chain more than two conditions:


All three conditions must be true for the message to print.

---



```kotlin
fun main() {
    val hasPassport = true
    val hasVisa = true
    val hasTicket = true

    if (hasPassport && hasVisa && hasTicket) {
        println("You're ready for international travel!")
    } else {
        println("Missing required documents")
    }
}
```
