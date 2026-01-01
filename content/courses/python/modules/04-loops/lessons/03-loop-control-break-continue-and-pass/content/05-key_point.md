---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **break** exits the loop immediately, skipping remaining iterations
- **continue** skips the rest of current iteration, moves to next
- **pass** does nothing (placeholder for future code)
- **Loop else** runs only if loop completes without break
- **Use break for**: Early exit, search operations, user-controlled loops
- **Use continue for**: Skipping invalid data, filtering, conditional processing
- **Use else for**: Search validation, avoiding flag variables
- **break/continue work in**: Both for and while loops
- **They only affect**: The innermost loop they're in

### Quick Reference:
```
# break: "Stop the loop"
for item in items:
    if found_it:
        break  # Exit loop

# continue: "Skip to next iteration"
for item in items:
    if skip_this:
        continue  # Next iteration
    process(item)

# pass: "Do nothing"
for item in items:
    if special_case:
        pass  # Placeholder
    else:
        process(item)

# else: "If loop wasn't broken"
for item in items:
    if item == target:
        break
else:
    print("Not found")  # Only if no break

```
### When to Use Each:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Scenario</th><th>Statement</th></tr><tr><td>Found what you need</td><td>break</td></tr><tr><td>Skip invalid/unwanted items</td><td>continue</td></tr><tr><td>Empty block placeholder</td><td>pass</td></tr><tr><td>Detect "not found" case</td><td>else</td></tr></table>### Before Moving On:
Make sure you can:

- Use break to exit loops early
- Use continue to skip iterations
- Understand when loop else executes
- Explain the difference between break and continue
- Choose the right control statement for each scenario

### Coming Up Next:
In **Lesson 4: Nested Loops**, you'll learn to:

- Put loops inside other loops
- Create 2D grids and patterns
- Process multi-dimensional data
- Combine break/continue with nesting
- Build multiplication tables, calendars, game boards

Nested loops unlock the power to work with grids, matrices, and complex data structures!