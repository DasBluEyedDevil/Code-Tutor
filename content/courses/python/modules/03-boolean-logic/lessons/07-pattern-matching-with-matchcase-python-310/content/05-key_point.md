---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **match/case requires Python 3.10+** - Check your version with `python --version`
- **Basic syntax:** `match subject:` followed by `case pattern:` blocks
- **Wildcard `_`** matches anything - use as default case (put it last!)
- **OR patterns with `|`** - `case "yes" | "y":` matches multiple values
- **Capture variables** - `case (x, y):` extracts values into x and y
- **Guards with `if`** - `case n if n > 0:` adds conditions to patterns
- **Sequence patterns** - Match lists/tuples with `[first, *rest]` syntax
- **Dictionary patterns** - Match and extract from dicts `{"key": value}`
- **Order matters** - First matching case wins, put specific cases before general ones

### When to Use match/case:

- Handling multiple command strings (start, stop, restart)
- Processing API responses with different types
- Parsing structured data (JSON, tuples)
- State machines with many states
- Menu systems with many options

### When to Use if/elif:

- Complex boolean conditions
- Conditions that aren't pattern-based
- Need to support Python < 3.10
- Only 2-3 simple conditions

### Real-World Use Cases:

1. **Command handlers:** `match command:` with cases for each command
2. **HTTP status codes:** `match response.status:` for different statuses
3. **Event processing:** `match event:` with cases for click, keypress, etc.
4. **Data validation:** Match expected structures, capture values
5. **Game logic:** Match player actions, game states