---
type: "KEY_POINT"
title: "Dictionary Key-Value Lookups"
---

## Key Takeaways

- **Dictionaries provide O(1) key lookups** -- `dict["key"]` is instant regardless of collection size. Use dictionaries when you need fast access by a unique identifier.

- **Always check before accessing** -- `dict["missing"]` throws `KeyNotFoundException`. Use `ContainsKey()`, `TryGetValue()`, or `GetValueOrDefault("key")` for safe access.

- **Keys must be unique** -- adding a duplicate key with `.Add()` throws an exception. Use the indexer `dict[key] = value` to insert-or-update safely.
