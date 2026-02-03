---
type: "KEY_POINT"
title: "Commenting Best Practices"
---

## Key Takeaways

- **Comments explain WHY, not WHAT** -- `// Calculate discount for loyalty members` is useful. `// Add 1 to x` is noise. Write comments that explain intent, not syntax.

- **Use `//` for quick notes, `/* */` for blocks** -- single-line comments are most common. Multi-line comments are useful for temporarily disabling code sections during debugging.

- **`///` XML comments generate documentation** -- use them on public methods and classes. Your IDE shows these as tooltips, and tools like DocFX generate API reference sites from them.
