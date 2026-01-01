---
type: "THEORY"
title: "Understanding the Concept"
---

Remember our Caesar salad analogy? A basic recipe is nice, but what if a customer wants EXTRA parmesan? Or NO croutons? What if they want a LARGE salad instead of regular?

**Parameters let you customize your functions!**

Think of parameters like form fields on an order:

- **Size:** small / medium / large
- **Extra cheese:** yes / no
- **Dressing:** on the side / mixed in

The function (recipe) stays the same, but the OUTPUT changes based on the INPUT you provide.

**In Python:**

```python
# Without parameters - always the same
def greet():
    print("Hello!")

# With parameters - customizable!
def greet(name):
    print(f"Hello, {name}!")

greet("Alice")  # Hello, Alice!
greet("Bob")    # Hello, Bob!
```

**And what about getting something BACK?**

Some functions just DO something (like `print()`). But others need to GIVE you something back - like a calculator that returns the answer. That's what **return values** are for!