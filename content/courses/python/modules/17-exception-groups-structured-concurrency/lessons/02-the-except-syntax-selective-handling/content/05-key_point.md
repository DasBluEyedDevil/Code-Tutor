---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **`except*`** handles exceptions inside ExceptionGroups
- The caught value is always an ExceptionGroup (even for one match)
- Multiple `except*` blocks can handle different types from the same group
- Unhandled exceptions are automatically re-raised
- Cannot mix `except*` with regular `except` in the same try block