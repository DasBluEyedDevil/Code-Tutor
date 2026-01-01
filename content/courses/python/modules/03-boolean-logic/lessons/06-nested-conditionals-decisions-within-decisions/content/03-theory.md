---
type: "THEORY"
title: "Syntax Breakdown"
---

### Nested Conditional Anatomy:
```
if outer_condition:           # Level 0 indentation
    outer_statements          # Level 1 (4 spaces)
    
    if inner_condition:       # Level 1 (4 spaces)
        inner_statements      # Level 2 (8 spaces)
    else:
        inner_else_statements # Level 2 (8 spaces)
else:
    outer_else_statements     # Level 1 (4 spaces)

```
#### Indentation Levels:

- **Level 0**: Main if (no indentation)
- **Level 1**: Code inside main if (4 spaces)
- **Level 2**: Code inside nested if (8 spaces)
- **Level 3**: Code inside triple-nested if (12 spaces)

#### Execution Flow Example:
```
age = 20
has_id = True

if age >= 18:              # Check outer condition
    print("Adult")         # Outer True: execute this
    
    if has_id:             # Now check inner condition
        print("ID verified")  # Inner True: execute this
    else:
        print("Need ID")   # (Skipped - inner was True)
else:
    print("Minor")         # (Skipped - outer was True)

# Output:
# Adult
# ID verified

```
### When to Use Nesting vs Logical Operators:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Use Nested if</th><th>Use Logical Operators (and/or)</th></tr><tr><td>When inner check only makes sense if outer is True</td><td>When both conditions are equal partners</td></tr><tr><td>When you need different actions at each level</td><td>When checking multiple requirements for one outcome</td></tr><tr><td>More readable for complex multi-level logic</td><td>Simpler and more concise for 2 conditions</td></tr></table>#### Example Comparison:
```
# Scenario 1: Use AND (both conditions equal)
# "Can only drive if 16+ AND have license"
if age >= 16 and has_license:
    print("Can drive")  # Simple, clear!

# Same with nesting (unnecessarily complex here):
if age >= 16:
    if has_license:
        print("Can drive")
# Nesting adds no value here

# Scenario 2: Use NESTING (different actions at each level)
# Different responses for logged in vs not, admin vs regular
if is_logged_in:
    if is_admin:
        show_admin_panel()  # Action 1
    else:
        show_user_panel()   # Action 2
else:
    show_login_page()       # Action 3
# Nesting is clearer here - 3 distinct outcomes

# Same with AND (awkward):
if is_logged_in and is_admin:
    show_admin_panel()
elif is_logged_in and not is_admin:
    show_user_panel()
else:
    show_login_page()
# Redundant checks, less clear

```
### Common Nesting Patterns:
#### 1. Layered Validation
```
if input_is_provided:
    if input_is_valid_format:
        if input_is_safe:
            process_input()

```
#### 2. Permission Checking
```
if is_logged_in:
    if is_admin or is_owner:
        allow_edit()
    else:
        deny_access()

```
#### 3. Tiered Eligibility
```
if age >= 18:
    if income >= 30000:
        if credit_score >= 650:
            approve_loan()

```
### Avoiding Deep Nesting (Readability):
**Problem:** Too many levels become hard to read:

```
# TOO DEEP (hard to follow):
if condition1:
    if condition2:
        if condition3:
            if condition4:
                if condition5:
                    do_something()  # 5 levels deep!

```
**Solution 1:** Use logical operators:

```
# BETTER:
if condition1 and condition2 and condition3 and condition4 and condition5:
    do_something()  # Flat, easier to read

```
**Solution 2:** Early returns (preview of functions):

```
# BETTER (for functions - you'll learn in Module 6):
if not condition1:
    return
if not condition2:
    return
do_something()  # Flatter logic

```
**Best Practice:** Keep nesting to 2-3 levels maximum for readability.

### Common Mistakes:

<li>**Wrong indentation levels**:```
# WRONG:
if outer:
  if inner:  # Only 2 spaces! Should be 4
      action

# CORRECT:
if outer:
    if inner:  # 4 spaces for outer, 4 more for inner
        action  # 8 spaces total

```
</li><li>**Forgetting the outer logic path**:```
# PROBLEM: What if not logged in?
if is_logged_in:
    if is_admin:
        show_admin()
    else:
        show_user()
# Missing else for outer if! What if not logged in?

# BETTER:
if is_logged_in:
    if is_admin:
        show_admin()
    else:
        show_user()
else:
    show_login()  # Handle all cases!

```
</li><li>**Nesting when AND would work**:```
# UNNECESSARILY NESTED:
if age >= 18:
    if has_license:
        allow_drive()

# SIMPLER:
if age >= 18 and has_license:
    allow_drive()

```
</li>