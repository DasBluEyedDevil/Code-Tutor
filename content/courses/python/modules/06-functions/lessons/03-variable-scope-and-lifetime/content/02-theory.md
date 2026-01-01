---
type: "THEORY"
title: "Local vs Global Variables"
---

**Local variables:**
- Created INSIDE a function
- Only exist WHILE the function runs
- Destroyed when the function ends
- Can't be accessed from outside the function

**Global variables:**
- Created OUTSIDE all functions
- Exist for the entire program
- Can be READ from anywhere
- To MODIFY them inside a function, you need the `global` keyword

```python
counter = 0  # Global variable

def increment():
    global counter  # Tell Python we want the global one
    counter += 1    # Now we can modify it

def show_count():
    print(counter)  # Can READ global without 'global' keyword

increment()
increment()
show_count()  # 2
```

**The LEGB Rule** (how Python looks for variables):
1. **L**ocal - Inside the current function
2. **E**nclosing - Inside any enclosing functions (nested functions)
3. **G**lobal - At the module level
4. **B**uilt-in - Python's built-in names (like `print`, `len`)

Python searches in this order. The first match wins!