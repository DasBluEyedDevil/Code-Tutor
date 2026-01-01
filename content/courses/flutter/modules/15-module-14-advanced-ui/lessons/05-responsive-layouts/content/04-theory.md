---
type: "THEORY"
title: "LayoutBuilder - Constraint-Based Layouts"
---


**LayoutBuilder** tells you the exact space available for a widget, regardless of screen size. This is more useful than MediaQuery for nested widgets.

**Why LayoutBuilder?**

MediaQuery gives you the full screen size. But what if your widget is inside a sidebar that's only 300px wide? MediaQuery still reports the full 1200px screen width.

LayoutBuilder gives you the **actual constraints** passed to your widget:

```dart
LayoutBuilder(
  builder: (context, constraints) {
    // constraints.maxWidth = actual available width
    // constraints.maxHeight = actual available height
    return YourWidget();
  },
)
```

**BoxConstraints Properties:**

| Property | Description |
|----------|-------------|
| `maxWidth` | Maximum width allowed |
| `maxHeight` | Maximum height allowed |
| `minWidth` | Minimum width required |
| `minHeight` | Minimum height required |
| `biggest` | Size(maxWidth, maxHeight) |
| `smallest` | Size(minWidth, minHeight) |

**When to Use Which:**

| Scenario | Use |
|----------|-----|
| Screen-level layouts | MediaQuery |
| Reusable components | LayoutBuilder |
| Widgets in scrollable areas | LayoutBuilder |
| Navigation decisions | MediaQuery |
| Grid column count | Either works |

