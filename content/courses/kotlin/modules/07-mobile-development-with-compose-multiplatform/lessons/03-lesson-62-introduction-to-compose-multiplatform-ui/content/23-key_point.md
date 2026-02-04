---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Compose UI is built from composable functions that emit UI elements**. Each composable describes what the UI should look like for the current state, and the framework handles efficient updates when state changes.

**Modifiers configure composable appearance and behavior**—size, padding, click handlers, colors. Chain modifiers to combine effects: `Modifier.size(100.dp).padding(16.dp).clickable { }`.

**Material Design components provide a complete UI toolkit** with built-in accessibility, theming, and platform conventions. Use `Button`, `TextField`, `Card`, and other Material components instead of building from primitives.
