---
type: "THEORY"
title: "Dynamic Text Scaling - Respecting User Preferences"
---


**Why Text Scaling Matters**

Users with low vision rely on system text size settings. When they increase text size:

- **Good apps** scale text and adjust layouts gracefully
- **Bad apps** clip text, overflow, or ignore the setting entirely

**System Text Scale Factors:**

| Platform | Default | Range |
|----------|---------|-------|
| iOS | 1.0 | 0.82 - 3.12 |
| Android | 1.0 | 0.85 - 2.0+ |

**Accessing Text Scale in Flutter:**

```dart
// Get the current text scale factor
final textScaler = MediaQuery.textScalerOf(context);

// Scale a font size
final scaledSize = textScaler.scale(16.0);

// Check if user prefers larger text
final prefersLargerText = textScaler.scale(1.0) > 1.2;
```

**Common Text Scaling Problems:**

1. **Fixed heights** - Text overflows containers with fixed heights
2. **Horizontal overflow** - Long text wraps unexpectedly or clips
3. **Icon/text misalignment** - Icons don't scale with text
4. **Layout breaking** - Row layouts overflow when text grows

**Best Practices:**

- Use `Flexible` and `Expanded` instead of fixed widths
- Set `minHeight` instead of fixed `height`
- Test with text scale at 2.0x
- Consider layout changes at large scales

