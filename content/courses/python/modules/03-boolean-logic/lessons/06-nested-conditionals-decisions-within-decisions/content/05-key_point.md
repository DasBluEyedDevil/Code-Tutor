---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Nested conditionals** = if statements inside other if statements
- **Indentation is critical**: Each level adds 4 spaces (4, 8, 12...)
- **Inner checks only run if outer checks pass** - layered decision making
- **Use nesting when**:
<li>Inner decision only makes sense if outer is True
- Different actions needed at each level
- Complex multi-layered logic

</li>- **Use AND/OR instead when**:
<li>Conditions are equal partners
- Simpler to express as one compound condition
- No different actions at intermediate levels

</li>- **Readability matters**: Keep nesting to 2-3 levels max
- **Common patterns**: Access control, layered validation, tiered eligibility, permission systems

### Nesting vs Logical Operators Decision Guide:
```
✅ Use NESTING when:
• Different actions at each level
• Inner check only relevant if outer passes
• Complex multi-tier logic (3+ outcomes)

✅ Use AND/OR when:
• All conditions are requirements for ONE outcome
• Simpler and flatter
• Two equal conditions

Example:
# AND is better here (both requirements for one outcome):
if age >= 16 and has_license:
    allow_drive()

# Nesting is better here (different actions at levels):
if is_logged_in:
    if is_admin:
        admin_panel()  # Action 1
    else:
        user_panel()   # Action 2
else:
    login_page()       # Action 3

```
### Module 3 Complete! 🎉
You've mastered ALL fundamental decision-making in Python:

- **Boolean values** (True/False)
- **Comparison operators** (==, !=, <, >, <=, >=)
- **Logical operators** (and, or, not)
- **if statements** (conditional execution)
- **if-else statements** (two paths)
- **elif chains** (multiple mutually exclusive paths)
- **Nested conditionals** (layered decisions)

These are the building blocks of EVERY decision a program makes. You can now write programs that adapt, respond, and make intelligent choices!

### What's Next: Module 4 - Loops
You've learned to make programs decide. Now you'll learn to make them **repeat**:

- **while loops**: Repeat while a condition is True
- **for loops**: Repeat for each item in a sequence
- **Loop control**: break, continue, else
- **Nested loops**: Loops inside loops

Loops + conditionals = the power to build almost anything!

**Before Module 4:** Take the Module 3 Quiz to test your understanding of all decision-making concepts.