---
type: "PITFALL"
title: "The Equality Trap"
---

### 1. The Only "Acceptable" Use for `==`
Some developers use `if (value == null)` as a shortcut to check if something is **either** `null` or `undefined`.
While this works, it’s often clearer for beginners to write:
`if (value === null || value === undefined)`

### 2. Don't believe everything you see
Because `==` is so flexible, it can trick you into thinking your data is one type when it’s actually another. If you use `==`, you might not realize that a variable you *think* is a number is actually a string, which will cause massive headaches when you try to do math later.

### 3. The Golden Rule
**Use `===` for everything.**
There is almost no situation in modern JavaScript where `==` is required, and the risks far outweigh the benefits. Most "Linters" (tools that check your code quality) will actually flag `==` as an error.