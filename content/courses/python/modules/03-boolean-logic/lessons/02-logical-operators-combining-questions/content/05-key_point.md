---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Three logical operators** combine Boolean questions:
  - `and` → All conditions must be True (strict gatekeeper)
  - `or` → At least one condition must be True (lenient)
  - `not` → Reverses True to False and vice versa

- **Truth tables** show all possible outcomes:
  - AND: Only True when ALL are True
  - OR: Only False when ALL are False
  - NOT: Always flips the value

- **Use parentheses** to make complex conditions clear and control order
- **Short-circuit evaluation**: Python stops checking once the answer is determined (efficient!)
- **Avoid redundancy**: Don't write `bool_var == True`, just use `bool_var`
- **Operator precedence**: `not` > `and` > `or` (when in doubt, use parentheses!)

### Common Patterns You'll Use Constantly:
```python
# Range checking (AND)
if 18 <= age <= 65:  # Chained comparison
    # Working age

# Multiple valid options (OR)
if status == "admin" or status == "owner":
    # Has full access

# Negation (NOT)
if not is_logged_in:
    # Show login page

# Complex conditions
if (is_member or spent >= 100) and has_coupon:
    # Apply special discount
```

### Before Moving On:
Make sure you can:

- Write AND conditions requiring all parts to be true
- Write OR conditions where at least one must be true
- Use NOT to reverse boolean values
- Use parentheses to combine operators correctly
- Read truth tables

### Coming Up Next:
In **Lesson 3: if Statements**, you'll finally learn how to USE these Boolean expressions to make your programs take different actions:

- Execute code only when conditions are true
- Skip code when conditions are false
- Indentation and code blocks
- Building programs that adapt to different situations

All the Boolean logic you've learned in Lessons 1 and 2 is about to become incredibly powerful!
