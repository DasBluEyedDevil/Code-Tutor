---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting the Return Statement**
```python
# WRONG - Function calculates but doesn't return!
def add(a, b):
    result = a + b
    # Forgot to return! Function returns None

total = add(5, 3)  # total is None, not 8!

# CORRECT - Return the result
def add(a, b):
    result = a + b
    return result  # Now the caller gets the value
```

**2. Confusing Print with Return**
```python
# WRONG - Printing instead of returning
def calculate_tax(amount):
    tax = amount * 0.1
    print(tax)  # This prints but returns None!

result = calculate_tax(100)  # result is None!
total = 100 + calculate_tax(100)  # Error! Can't add None

# CORRECT - Return the value
def calculate_tax(amount):
    tax = amount * 0.1
    return tax  # Caller can use this value

result = calculate_tax(100)  # result is 10.0
```

**3. Wrong Parameter Order**
```python
# WRONG - Arguments in wrong order
def divide(numerator, denominator):
    return numerator / denominator

result = divide(2, 10)  # Returns 0.2, not 5!

# CORRECT - Use keyword arguments for clarity
result = divide(numerator=10, denominator=2)  # Returns 5.0
```

**4. Missing Required Arguments**
```python
# WRONG - Forgetting to pass required arguments
def greet(first_name, last_name):
    return f"Hello, {first_name} {last_name}!"

message = greet("Alice")  # TypeError: missing required argument!

# CORRECT - Pass all required arguments
message = greet("Alice", "Smith")  # Works!
```

**5. Code After Return Never Executes**
```python
# WRONG - Code after return is never reached
def process(x):
    return x * 2
    print("Processing complete!")  # This NEVER runs!
    x = x + 1  # This NEVER runs either!

# CORRECT - Put all logic before return
def process(x):
    result = x * 2
    print("Processing complete!")
    return result  # Return should be last
```