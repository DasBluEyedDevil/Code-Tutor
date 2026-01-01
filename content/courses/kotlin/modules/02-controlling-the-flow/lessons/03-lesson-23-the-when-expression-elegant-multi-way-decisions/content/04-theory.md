---
type: "THEORY"
title: "Basic When Expression"
---


### Syntax and Structure


**Parts:**
- `when` - Keyword starting the expression
- `(expression)` - The value to match against
- `value ->` - Match condition followed by arrow
- `result` - What to return/execute when matched
- `else` - Default case (like the final "otherwise")

### Your First When Expression


**Output:**

**How it works:**
1. Evaluate the expression: `trafficLight` = "Red"
2. Check each branch from top to bottom
3. Find match: `"Red"` matches first branch
4. Return result: `"Stop"`
5. Assign to `action` variable
6. Skip remaining branches

---



```kotlin
Traffic light is Red: Stop
```
