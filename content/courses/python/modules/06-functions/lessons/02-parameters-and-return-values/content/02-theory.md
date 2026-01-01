---
type: "THEORY"
title: "Parameters vs. Return Values"
---

**Parameters** = What goes INTO the function (the ingredients)
**Return values** = What comes OUT of the function (the result)

```python
# Parameters: a and b go IN
# Return value: the sum comes OUT
def add(a, b):
    return a + b

result = add(5, 3)  # result = 8
```

**The `return` keyword:**

- Sends a value back to wherever the function was called
- Immediately exits the function (code after `return` won't run)
- If there's no `return`, the function returns `None` automatically

**Multiple parameters:**

```python
def create_email(username, domain):
    return f"{username}@{domain}"

email = create_email("john", "gmail.com")  # john@gmail.com
```

**Using the return value:**

```python
# Store it in a variable
result = add(5, 3)

# Use it directly in another expression
total = add(5, 3) + add(2, 1)  # 8 + 3 = 11

# Use it in a print statement
print(f"The sum is {add(5, 3)}")
```