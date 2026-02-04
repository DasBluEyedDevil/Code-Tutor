---
type: KEY_POINT
---

- `if` evaluates a boolean condition and runs its block only when the condition is `true`
- Chain `else if` for multiple conditions; the first matching branch executes and the rest are skipped
- `switch` expressions (Dart 3) return a value directly and the compiler warns about unhandled cases, making them safer than `if/else` chains
- Comparison operators (`==`, `!=`, `<`, `>`, `<=`, `>=`) and logical operators (`&&`, `||`, `!`) combine to form complex conditions
- Guard your `else` branches -- always handle the "otherwise" case to prevent silent bugs when no condition matches
