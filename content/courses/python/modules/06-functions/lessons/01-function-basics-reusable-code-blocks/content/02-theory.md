---
type: "THEORY"
title: "Anatomy of a Function"
---

Let's break down the parts of a function:

```python
def greet():
    print("Hello, welcome!")
    print("Nice to meet you!")
```

**The pieces:**

- **`def`** - This keyword tells Python "I'm defining a function"
- **`greet`** - This is the function's name (you choose it!)
- **`()`** - Parentheses hold parameters (empty for now, we'll add these later)
- **`:`** - The colon marks the start of the function's body
- **Indented code** - Everything indented under `def` is PART of the function

**Defining vs. Calling:**

```python
# DEFINING the function (creating the recipe)
def greet():
    print("Hello!")

# CALLING the function (using the recipe)
greet()  # This actually runs the code inside
greet()  # You can call it as many times as you want!
```

**Important:** When Python sees `def`, it doesn't run the code inside - it just remembers it. The code only runs when you CALL the function by writing its name followed by `()`.