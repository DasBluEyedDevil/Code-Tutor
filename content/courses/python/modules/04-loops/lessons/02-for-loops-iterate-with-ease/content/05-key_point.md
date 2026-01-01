---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **for loops iterate over sequences** - no manual counter needed!
- **Syntax**: `for variable in sequence:`
- **range() creates number sequences**:
<li>`range(stop)` → 0 to stop-1
- `range(start, stop)` → start to stop-1
- `range(start, stop, step)` → custom increment/decrement

</li>- **Stop value is excluded**: `range(1, 6)` gives 1,2,3,4,5 (not 6!)
- **Iterate strings**: `for char in "Hello":` loops through each letter
- **Nested loops**: Loop inside loop for 2D patterns, grids
- **for vs while**:
<li>for: Known sequence, simpler, less error-prone
- while: Unknown iterations, condition-based

</li>- **Use `end=""`** to print without newline
- **Common patterns**: Counting, accumulating, building strings, character processing

### range() Quick Reference:
```
range(5)           # 0, 1, 2, 3, 4
range(1, 6)        # 1, 2, 3, 4, 5
range(0, 10, 2)    # 0, 2, 4, 6, 8
range(10, 0, -1)   # 10, 9, 8, ..., 1
range(1, 10, 3)    # 1, 4, 7

```
### Before Moving On:
Make sure you can:

- Write a for loop with range() in all three forms
- Iterate over strings
- Use nested loops for patterns
- Choose between for and while appropriately
- Avoid off-by-one errors with range()

### Coming Up Next:
In **Lesson 3: Loop Control (break, continue, pass)**, you'll learn to:

- **break**: Exit a loop early
- **continue**: Skip to next iteration
- **pass**: Placeholder for empty loops
- **else with loops**: Code that runs if loop completes normally

These give you fine-grained control over loop execution - stopping early, skipping iterations, and detecting normal vs broken loops!