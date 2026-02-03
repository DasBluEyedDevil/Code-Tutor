---
type: "KEY_POINT"
title: "String Interpolation Over Concatenation"
---

## Key Takeaways

- **Prefer `$"Hello, {name}!"` over `"Hello, " + name + "!"`** -- string interpolation is more readable, less error-prone, and optimized by the compiler in .NET 6+.

- **Watch operator precedence with `+`** -- `"Result: " + 2 + 2` gives `"Result: 22"` (string concatenation), but `"Result: " + (2 + 2)` gives `"Result: 4"` (math first). Use parentheses to be explicit.

- **Interpolation supports formatting** -- `$"{price:C}"` for currency, `$"{date:yyyy-MM-dd}"` for dates. Format specifiers go after a colon inside the braces.
