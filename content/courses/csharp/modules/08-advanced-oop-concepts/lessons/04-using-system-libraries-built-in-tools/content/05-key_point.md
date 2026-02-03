---
type: "KEY_POINT"
title: "BCL Essentials: Files, Dates, Math"
---

## Key Takeaways

- **`File.ReadAllText()` and `File.WriteAllText()` handle simple I/O** -- for larger files, use `StreamReader`/`StreamWriter`. Always use `Path.Combine()` for cross-platform file paths.

- **Use `DateOnly` and `TimeOnly` when you do not need both** -- these .NET 6+ types are clearer than `DateTime` when modeling dates without times (birthdays) or times without dates (store hours).

- **`StringBuilder` is essential for string loops** -- concatenating strings in a loop creates a new string object each iteration. `StringBuilder.Append()` modifies in place, dramatically improving performance.
