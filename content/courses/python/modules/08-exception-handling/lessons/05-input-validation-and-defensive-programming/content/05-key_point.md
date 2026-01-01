---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Always validate user input** - never trust it! Check type, format, range, and empty/null values before using input.
- **Defensive programming:** Assume everything can go wrong. Validate parameters, check types, handle edge cases, provide defaults.
- **Validation order:** (1) Check not None/empty, (2) Check type with isinstance(), (3) Sanitize (strip, lower), (4) Check format/range.
- **EAFP (Easier to Ask Forgiveness than Permission)** is more Pythonic than LBYL. Try the operation, catch exceptions if they occur.
- **LBYL (Look Before You Leap)** is okay for simple checks (if key in dict:) where readability matters more than performance.
- **Type checking:** Use isinstance(value, type) to check types. Can check multiple types: isinstance(x, (int, float)).
- **Provide actionable error messages:** Tell users what's wrong, what's expected, and what they provided. 'Age must be 13-120, got 10' beats 'Invalid age'.
- **Sanitize input:** Use .strip() to remove whitespace, .lower() for case-insensitive comparison. But DON'T strip passwords!
- **Range validation:** Always check numeric ranges (0 <= age <= 120), string lengths (3 <= len(username) <= 20), and limits.