---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Exceptions are runtime errors** that happen when code runs (not syntax errors). Without handling, they crash your program.
- **try/except blocks** let you catch exceptions and handle them gracefully instead of crashing. The program continues running after the except block.
- **try block** contains risky code that might fail. **except block** contains the code to run if an error occurs.
- **Always specify the exception type** in except blocks (like ValueError, ZeroDivisionError). Don't use bare except: clauses.
- **Common exceptions:** ValueError (invalid data conversion), ZeroDivisionError (dividing by zero), TypeError (wrong data type).
- **Error handling makes programs robust** - they can handle unexpected situations without crashing, providing better user experience.
- **The flow:** Try runs first → if error, jump to except → after except, program continues normally. If no error, except is skipped entirely.