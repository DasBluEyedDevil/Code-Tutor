---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Type hints are optional** - Not enforced at runtime, for tooling/documentation
- **Modern syntax (3.9+):** `list[str]` instead of `List[str]`
- **Union operator (3.10+):** `X | None` instead of `Optional[X]`
- **type statement (3.12+):** `type Alias = ...` for explicit type aliases
- **TypedDict:** Define exact dict structure for JSON/API data
- **Protocol:** Structural typing (duck typing with type safety)
- **Use type checkers** - mypy, pyright catch errors before running
- **Best practices:** Use modern syntax, TypedDict for structured data, Protocol for interfaces