---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Default arguments** provide fallback values: `def greet(name, greeting="Hello"):`
- **Parameters with defaults** must come AFTER parameters without defaults
- **Keyword arguments** specify values by name: `greet(name="Alice", greeting="Hi")`
- **Keyword arguments** can be in any order
- **Positional arguments** must come before keyword arguments in function calls
- **Best practice**: Use defaults for optional parameters, required parameters should have no default
- **Readable code**: Use keyword arguments when calling functions with many parameters
- **Common pattern**: `def func(required1, required2, optional1="default", optional2=None):`
- **Avoid mutable defaults**: Don't use `def func(items=[])` - use `def func(items=None):` instead