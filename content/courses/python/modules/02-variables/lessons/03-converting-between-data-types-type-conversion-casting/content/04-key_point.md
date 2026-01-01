---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Type conversion** (casting) changes data from one type to another
- Three main conversion functions: `int()`, `float()`, `str()`
- `input()` always returns a **string**, even if the user types a number
- `int()` converts to whole numbers and **truncates** (doesn't round) decimals
- `float()` converts to decimal numbers
- `str()` converts anything to text (for combining with strings)
- **Implicit conversion:** Python auto-converts in math (int + float = float)
- **Explicit conversion:** You tell Python to convert with int(), float(), str()
- Always convert **before** doing math on user input
- Use `type()` to check what type a variable is
- Format money with `:.2f` for exactly 2 decimal places
- Division (`/`) always returns a float, even with integers
- Common error: `ValueError` when trying to convert invalid strings like `int("hello")`
- Choose the right type: int for counts, float for measurements/prices