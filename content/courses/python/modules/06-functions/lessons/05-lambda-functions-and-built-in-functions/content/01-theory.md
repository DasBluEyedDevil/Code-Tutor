---
type: "THEORY"
title: "Understanding the Concept"
---

Sometimes you need a quick, throwaway function - something so simple that giving it a name feels like overkill. It's like writing a full recipe for "squeeze lemon" when you're just making lemonade.

**Lambda functions** are anonymous (nameless) mini-functions written in one line:

```python
# Regular function
def double(x):
    return x * 2

# Same thing as a lambda
double = lambda x: x * 2

# Both work the same way!
print(double(5))  # 10
```

**The syntax:**
```python
lambda parameters: expression
```

- `lambda` - The keyword that starts it
- `parameters` - Input values (like regular function parameters)
- `expression` - What to return (no `return` keyword needed!)

**When to use lambdas:**
- Simple, one-line operations
- When passing a function to another function (like `sorted()`, `map()`, `filter()`)
- Quick data transformations

**When NOT to use lambdas:**
- Complex logic (use regular functions)
- When you need multiple statements
- When the function needs a descriptive name for clarity