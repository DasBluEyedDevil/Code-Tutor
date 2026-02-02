---
type: "THEORY"
title: "Understanding except*"
---

The `except*` syntax (note the asterisk!) is designed specifically for handling ExceptionGroups.

**Key difference from `except`:**
- `except ValueError` - catches a single ValueError
- `except* ValueError` - catches all ValueErrors **inside an ExceptionGroup**

**How it works:**
1. Python examines each exception in the group
2. Matches are extracted and handled
3. Non-matches are re-raised in a new ExceptionGroup
4. Multiple `except*` blocks can each handle different types

**Important:** `except*` and regular `except` cannot be mixed in the same try block.