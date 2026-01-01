---
type: "THEORY"
title: "MediaQuery - Screen Size, Orientation, and More"
---


**MediaQuery** provides information about the current screen and user preferences:

**Common Properties:**

| Property | Description | Example Value |
|----------|-------------|---------------|
| `size` | Screen dimensions | Size(375.0, 812.0) |
| `orientation` | Portrait/Landscape | Orientation.portrait |
| `devicePixelRatio` | Pixel density | 3.0 (retina) |
| `textScaleFactor` | User's text size preference | 1.0 (default) |
| `platformBrightness` | Light/Dark mode | Brightness.dark |
| `padding` | Safe area insets | EdgeInsets (notch, etc.) |
| `viewInsets` | Keyboard/overlay insets | EdgeInsets |

**Accessing MediaQuery:**

```dart
// Full MediaQueryData object
final mediaQuery = MediaQuery.of(context);

// Specific properties (more efficient)
final width = MediaQuery.sizeOf(context).width;
final orientation = MediaQuery.orientationOf(context);
final textScale = MediaQuery.textScaleFactorOf(context);
```

**Performance Note:**

Use the specific `*Of` methods when you only need one property. They only rebuild when that specific property changes:

```dart
// BAD - Rebuilds on ANY MediaQuery change
final size = MediaQuery.of(context).size;

// GOOD - Only rebuilds when size changes
final size = MediaQuery.sizeOf(context);
```

