---
type: "KEY_POINT"
title: "Answer Key"
---


**Question 1: B** - `ColorScheme.fromSeed()` is the Material 3 way. It generates a complete, harmonious 30-color palette from a single seed color automatically.

**Question 2: B** - `Theme.of(context)` returns the nearest ThemeData in the widget tree, allowing you to access theme colors, text styles, and other styling information.

**Question 3: D** - `labelLarge` is specifically designed for button text in Material 3. It has appropriate sizing and weight for button labels.

**Question 4: B** - Define both `theme` (light) and `darkTheme` (dark) in MaterialApp, then use `themeMode` to control which is active. Flutter handles the switching automatically.

**Question 5: C** - Use theme colors for consistency. Hardcoded colors should be rare exceptions. For error states, use `Theme.of(context).colorScheme.error` instead of `Colors.red`.

