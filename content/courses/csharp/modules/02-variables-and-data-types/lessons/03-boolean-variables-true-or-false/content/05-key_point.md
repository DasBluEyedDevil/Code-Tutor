---
type: "KEY_POINT"
title: "Boolean Variables and Naming"
---

## Key Takeaways

- **Booleans hold only `true` or `false`** -- no quotes, no capitalization. They are keywords, not strings. `bool isReady = true;` is correct; `"true"` is a string, not a boolean.

- **Name booleans as yes/no questions** -- prefix with `is`, `has`, `can`, or `should`: `isActive`, `hasPermission`, `canEdit`, `shouldRetry`. This makes `if` statements read like English.

- **Comparisons produce booleans** -- `score >= 60` evaluates to `true` or `false`. You can store this result directly: `bool isPassing = score >= 60;` instead of writing an if/else.
