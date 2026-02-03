---
type: "KEY_POINT"
title: "Multi-Line Output and Escape Sequences"
---

## Key Takeaways

- **Use `\n` for newlines inside a single string** -- `Console.WriteLine("Line1\nLine2")` prints two lines. Multiple `WriteLine` calls also work but are more verbose.

- **Escape sequences are special character combinations** -- `\t` (tab), `\\` (literal backslash), `\"` (literal quote). C# 13 adds `\e` for terminal color codes.

- **Choose readability over cleverness** -- multiple `WriteLine` calls are clearer for beginners; escape sequences are more compact for experienced developers. Pick whichever your team can read faster.
