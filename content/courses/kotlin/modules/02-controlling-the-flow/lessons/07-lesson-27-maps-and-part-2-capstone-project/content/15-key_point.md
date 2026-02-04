---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Maps store key-value pairs for fast lookups by key**. Use maps when you need to associate data (like IDs to users, words to definitions, or configuration names to values) instead of searching through lists.

**Access map values with bracket notation or safe accessors**: `map[key]` returns nullable `V?` while `map.getValue(key)` throws an exception if the key is missing. Choose based on whether missing keys are exceptional or expected.

**The Part 2 Capstone demonstrates data structures in action**—combining lists and maps to build a practical application. Real-world software is built from composing simple structures like these into complex systems.
