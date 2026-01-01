---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Creating a module:**
```python
# my_module.py
def my_function():
    return "Hello"

MY_CONSTANT = 42
```

**Using your module:**
```python
# main.py
import my_module
result = my_module.my_function()
```

**The __name__ == '__main__' pattern:**
```python
# my_module.py
def helper():
    return "I'm a helper"

if __name__ == "__main__":
    # Runs only when: python my_module.py
    # Does NOT run when: import my_module
    print("Testing module...")
    print(helper())
```

**Why use __name__ == '__main__'?**
- Test code while developing
- Demo module functionality
- Provide CLI interface
- Code runs when executed directly, not when imported