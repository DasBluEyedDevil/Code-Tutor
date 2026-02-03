---
type: "KEY_POINT"
title: "Records for Immutable Data"
---

## Key Takeaways

- **Records provide value equality** -- two records with identical data are equal (`==`), even if they are different objects. Classes compare by reference (same memory location).

- **The `with` expression creates modified copies** -- `person with { Age = 31 }` returns a new record; the original is unchanged. This is how you "update" immutable data safely.

- **One-line record declarations generate everything** -- `public record Person(string Name, int Age);` creates properties, constructor, ToString, Equals, GetHashCode, and a deconstructor automatically.
