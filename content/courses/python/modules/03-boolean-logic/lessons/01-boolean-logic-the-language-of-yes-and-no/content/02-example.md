---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
Is it raining? True
Is it sunny? False
Data type: <class 'bool'>

Is age exactly 25? True
Is age exactly 30? False
Is age NOT 30? True
Is age greater than min_age? True
Is age less than min_age? False
Is age >= min_age? True
Is age <= 30? True

Price: $45
Qualifies for free shipping? False
```

```python
# Boolean Values: True and False
# These are actual values in Python, just like numbers or strings

is_raining = True
is_sunny = False

print("Is it raining?", is_raining)  # Output: True
print("Is it sunny?", is_sunny)       # Output: False
print("Data type:", type(is_raining)) # Output: <class 'bool'>

print()

# Comparison Operators: Asking Questions
# These operators RETURN boolean values (True or False)

age = 25
min_age = 18

# Equal to (==)
print("Is age exactly 25?", age == 25)      # True
print("Is age exactly 30?", age == 30)      # False

# Not equal to (!=)
print("Is age NOT 30?", age != 30)           # True

# Greater than (>)
print("Is age greater than min_age?", age > min_age)  # True (25 > 18)

# Less than (<)
print("Is age less than min_age?", age < min_age)     # False (25 is not < 18)

# Greater than or equal (>=)
print("Is age >= min_age?", age >= min_age)  # True (25 >= 18)

# Less than or equal (<=)
print("Is age <= 30?", age <= 30)            # True (25 <= 30)

print()

# Booleans in Action: Making Decisions
price = 45
free_shipping_threshold = 50

qualifies_for_free_shipping = price >= free_shipping_threshold
print(f"Price: ${price}")
print(f"Qualifies for free shipping?", qualifies_for_free_shipping)
# Output: False (because 45 < 50)
```
