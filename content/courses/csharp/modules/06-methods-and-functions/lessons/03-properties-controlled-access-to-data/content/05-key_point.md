---
type: "KEY_POINT"
title: "Properties for Controlled Access"
---

## Key Takeaways

- **Properties replace public fields** -- `public int Age { get; set; }` provides controlled access with optional validation in the setter. Never expose fields directly.

- **`init` (C# 9+) creates set-once properties** -- `{ get; init; }` allows assignment only during object creation. Combine with `required` (C# 11+) to force callers to provide a value.

- **Expression-bodied properties are read-only shorthand** -- `public string FullName => $"{First} {Last}";` computes the value on access. No backing field needed.
