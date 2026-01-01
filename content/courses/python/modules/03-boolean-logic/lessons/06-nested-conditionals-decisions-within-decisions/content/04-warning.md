---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Wrong Indentation Levels**
```python
# WRONG - Inconsistent indentation
if is_logged_in:
    if is_admin:
      print("Admin")  # Only 2 spaces - should be 8!

# CORRECT - Each level adds 4 spaces
if is_logged_in:
    if is_admin:
        print("Admin")  # 8 spaces (4 + 4)
```

**2. Forgetting to Handle All Paths**
```python
# WRONG - What happens if not logged in?
if is_logged_in:
    if is_admin:
        show_admin_panel()
    else:
        show_user_panel()
# No else for outer if - user sees nothing!

# CORRECT - Handle all cases
if is_logged_in:
    if is_admin:
        show_admin_panel()
    else:
        show_user_panel()
else:
    show_login_page()  # Handle the outer False case
```

**3. Unnecessary Nesting (Use AND Instead)**
```python
# OVERLY COMPLEX
if age >= 18:
    if has_license:
        if not is_suspended:
            allow_drive()

# SIMPLER - Use AND for single outcome
if age >= 18 and has_license and not is_suspended:
    allow_drive()
```

**4. Too Many Nesting Levels (Hard to Read)**
```python
# TOO DEEP - Hard to follow!
if a:
    if b:
        if c:
            if d:
                if e:
                    do_something()  # 5 levels deep!

# BETTER - Refactor with early returns or AND
if not a or not b or not c:
    return  # Exit early
if d and e:
    do_something()
```

**5. Confusing Else Matching**
```python
# CONFUSING - Which if does this else match?
if condition1:
    if condition2:
        action1()
else:  # Matches inner if or outer if?
    action2()

# Python matches else with the nearest if
# The else above matches condition2, not condition1!
# Add comments or restructure to clarify intent
```