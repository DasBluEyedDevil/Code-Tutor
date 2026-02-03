---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
Nightclub entry: True
Age check: True
ID check: True
Not banned: True
Final result (AND): True

Can sleep in? True
Can sleep in? (weekday) False

Is raining? True
Can go outside without umbrella? False

Age 15, Student: True
Gets discount? True

Short-circuit AND: False
Short-circuit OR: True
```

```python
# Logical Operators: and, or, not
# Combine Boolean questions for complex decision-making

# Example 1: AND - All conditions must be True
age = 25
has_id = True
is_banned = False

# Nightclub bouncer logic
can_enter = age >= 21 and has_id and not is_banned
print("Nightclub entry:", can_enter)  # True (all conditions met)

# Breaking it down:
print("Age check:", age >= 21)        # True (25 >= 21)
print("ID check:", has_id)            # True
print("Not banned:", not is_banned)   # True (not False = True)
print("Final result (AND):", age >= 21 and has_id and not is_banned)  # True

print()

# Example 2: OR - At least one condition must be True
is_weekend = True
is_holiday = False

can_sleep_in = is_weekend or is_holiday
print("Can sleep in?", can_sleep_in)  # True (weekend is True)

# Both false example:
is_weekend = False
is_holiday = False
can_sleep_in = is_weekend or is_holiday
print("Can sleep in? (weekday)", can_sleep_in)  # False (both False)

print()

# Example 3: NOT - Reverses True/False
is_raining = True
should_bring_umbrella = is_raining
can_go_outside_without_umbrella = not is_raining

print("Is raining?", is_raining)      # True
print("Can go outside without umbrella?", can_go_outside_without_umbrella)  # False

print()

# Example 4: Complex Combinations
# Movie ticket pricing logic
age = 15
is_student = True
is_senior = False

# Gets discount if: (child OR senior) OR (student AND weekday)
is_child = age < 13
is_weekday = True

gets_discount = (is_child or is_senior) or (is_student and is_weekday)
print(f"Age {age}, Student: {is_student}")
print("Gets discount?", gets_discount)  # True (student on weekday)

print()

# Example 5: Short-circuit evaluation (advanced)
# Python stops checking as soon as it knows the answer
x = 5
y = 10

# AND: If first is False, doesn't check the second
result = (x > 10) and (y > 5)  # Python sees x > 10 is False, stops there
print("Short-circuit AND:", result)  # False (didn't need to check y)

# OR: If first is True, doesn't check the second
result = (x < 10) or (y > 100)  # Python sees x < 10 is True, stops there
print("Short-circuit OR:", result)  # True (didn't need to check y)
```