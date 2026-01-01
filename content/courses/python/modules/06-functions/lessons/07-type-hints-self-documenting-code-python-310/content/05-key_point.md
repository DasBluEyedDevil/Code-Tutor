---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Type hints are documentation** - They tell developers what types to use without reading implementation
- **Basic syntax:** `param: type` for parameters, `-> type` for return values
- **Common types:** `str`, `int`, `float`, `bool`, `None`
- **Collection types (Python 3.9+):** `list[int]`, `dict[str, int]`, `set[str]`, `tuple[int, str]`
- **Optional values (Python 3.10+):** Use `str | None` instead of `Optional[str]` - cleaner, no import needed
- **Union types (Python 3.10+):** Use `str | int` instead of `Union[str, int]` - cleaner, no import needed
- **Type hints are optional** - Python doesn't enforce them at runtime
- **Tools like mypy** - Static type checkers catch type errors before running
- **IDE benefits** - Better autocomplete, error detection, refactoring support
- **Modern Python style** - Use `|` syntax for unions and optionals in Python 3.10+

### When to Use Type Hints:

- Public functions and methods (APIs others will call)
- Complex functions with multiple parameters
- Functions where the return type isn't obvious
- Codebases shared with other developers
- When you want IDE autocomplete to work better

### When Type Hints Might Be Overkill:

- Simple one-liner internal functions
- Scripts you'll use once
- Prototyping and experimentation
- When types are completely obvious from context