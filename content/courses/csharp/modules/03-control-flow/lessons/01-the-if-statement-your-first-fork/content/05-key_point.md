---
type: "KEY_POINT"
title: "If Statement Fundamentals"
---

## Key Takeaways

- **The condition must evaluate to a boolean** -- `if (age >= 21)` works because `>=` produces `true` or `false`. You cannot write `if (age)` in C# like some other languages -- be explicit.

- **Always use curly braces** -- even for single-line bodies. `if (x > 0) { DoSomething(); }` prevents bugs when you later add a second line that you expect to be inside the block.

- **Code after the if block always runs** -- only the code inside the braces is conditional. Everything below the closing `}` executes regardless of whether the condition was true or false.
