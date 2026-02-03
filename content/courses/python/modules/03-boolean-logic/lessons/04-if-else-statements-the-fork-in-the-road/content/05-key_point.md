---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **if-else ensures complete coverage**: Exactly ONE of the two blocks always executes
- **Syntax**: `if condition:` then `else:` (no condition on else!)
- **Mutually exclusive**: if block runs when True, else block runs when False - never both
- **else alignment**: Must be at same indentation level as its if
- **else colon**: Don't forget the : after else
- **Best for binary decisions**: When you have exactly two options (yes/no, pass/fail, on/off)
- **More efficient than multiple if**: Only one condition check instead of two
- **Guaranteed execution**: Variables set in both blocks will always be defined

### When to Use if-else:
✅ **Use if-else when:**
- Exactly two mutually exclusive outcomes
- Both True and False cases need explicit handling
- Binary decisions (on/off, yes/no, valid/invalid)

❌ **Don't use if-else when:**
- More than two possible outcomes (use elif - next lesson!)
- The False case needs no action (just use if)
- Conditions are independent (use multiple if statements)

### Before Moving On:
Make sure you can:

- Write if-else with proper syntax and indentation
- Explain why exactly one block always runs
- Identify when if-else is better than multiple if statements
- Set variables inside both blocks

### Coming Up Next:
In **Lesson 5: elif Chains**, you'll learn to handle **more than two options**:

- Checking multiple mutually exclusive conditions
- Grade calculator (A, B, C, D, F)
- Menu systems with many options
- Combining if-elif-else for complete coverage

if-else handles 2 options. elif chains handle 3, 4, 5... as many as you need!
