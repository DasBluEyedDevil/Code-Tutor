---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Common exceptions:** ValueError (wrong value), TypeError (wrong type), IndexError (list index), KeyError (dict key), ZeroDivisionError, FileNotFoundError, AttributeError (missing attribute/method).
- **Catch specific exceptions** you expect, not the generic Exception class. Specific catches make debugging easier and handling more precise.
- **Multiple except blocks:** Use separate blocks for different exception types when you need different handling for each.
- **Group exceptions:** Use except (ValueError, TypeError) when you want the same handling for multiple exception types.
- **Order matters:** Put specific exceptions BEFORE general ones. Python checks except blocks in order and uses the first match.
- **Use 'as e' to capture exception details:** except ValueError as e: lets you access the error message and other useful information.
- **Avoid bare except:** except Exception catches almost everything; except: catches EVERYTHING including Ctrl+C. Both hide bugs and make debugging hard.
- **Exception hierarchy:** Exception is the parent class of most errors. Catching it catches all its children (ValueError, TypeError, etc.).