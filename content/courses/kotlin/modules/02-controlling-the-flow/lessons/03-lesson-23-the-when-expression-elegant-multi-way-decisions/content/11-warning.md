---
type: "WARNING"
title: "Common Pitfalls and Best Practices"
---


### Pitfall 1: Missing else in Expressions

❌ **Error:**

✅ **Correct:**

**Rule:** When used as an expression (returning a value), `else` is required unless the compiler can prove all cases are covered.

### Pitfall 2: Overlapping Ranges

❌ **Problem:**

The second range is completely covered by the first. `when` executes the **first** matching branch.

✅ **Correct:**

### Pitfall 3: Forgetting Braces for Multiple Statements

❌ **Won't compile:**

✅ **Correct:**

### Best Practice 1: Order Matters

Put the most specific cases first:

✅ **Good:**

### Best Practice 2: Use When for 3+ Options

- **2 options:** Use `if-else`
- **3+ options:** Use `when`


### Best Practice 3: Exhaustive When

For enums and sealed classes, you can make `when` exhaustive without `else`:


---



```kotlin
enum class Direction { NORTH, SOUTH, EAST, WEST }

fun move(direction: Direction) = when (direction) {
    Direction.NORTH -> "Going north"
    Direction.SOUTH -> "Going south"
    Direction.EAST -> "Going east"
    Direction.WEST -> "Going west"
    // No else needed - all cases covered!
}
```
