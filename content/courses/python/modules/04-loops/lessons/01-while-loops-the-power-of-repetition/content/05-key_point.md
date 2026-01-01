---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **while loops repeat code as long as a condition is True**
- **Syntax**: `while condition:` then indented body
- **Three parts**: Initialize (before loop), Condition (in while), Update (inside loop)
- **Loop execution**: Check condition → If True, run body → Loop back → Check again
- **Must update loop variable** or you get an infinite loop!
- **Common patterns**:
<li>Counter: count to N
- Sentinel: loop until specific input
- Flag: Boolean variable controls loop
- Validation: repeat until valid input
- Accumulator: build up a result

</li>- **Infinite loops happen when** you forget to update the variable in the condition
- **Stop infinite loop**: Press Ctrl+C

### When to Use while Loops:
```
✅ Use while when:
• You don't know how many times to repeat ("until user quits")
• Condition is based on user input or changing state
• Input validation (retry until valid)
• Event-driven loops (game loops, menu systems)

❌ Don't use while when:
• You know exactly how many times to repeat (use for loop - next lesson!)
• Iterating over a sequence (use for loop)

```
### Before Moving On:
Make sure you can:

- Write a basic while loop with proper syntax
- Initialize variables before the loop
- Update loop variables inside the loop
- Avoid infinite loops by ensuring the condition eventually becomes False
- Use while loops for input validation and counters

### Coming Up Next:
In **Lesson 2: for Loops**, you'll learn Python's OTHER loop type:

- Iterating over sequences (strings, lists, ranges)
- `range()` function for counting
- Cleaner syntax when you know the iterations
- The difference between for and while

while loops = "repeat WHILE condition is True"
for loops = "repeat FOR each item in a sequence"

Together, these two loop types handle every repetition scenario!