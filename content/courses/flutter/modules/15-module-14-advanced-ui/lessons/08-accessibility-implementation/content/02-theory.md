---
type: "THEORY"
title: "Focus Indicators - Making Focus Visible"
---


**The Problem with Invisible Focus**

Many custom widgets lose the default focus indicator, leaving keyboard users stranded:

```dart
// BAD: Custom widget with no focus indicator
GestureDetector(
  onTap: doSomething,
  child: Container(
    padding: EdgeInsets.all(16),
    child: Text('Click me'),
  ),
)
```

Keyboard users Tab to this widget but see nothing. They don't know it's focused.

**WCAG Focus Requirements:**

| Criterion | Requirement |
|-----------|-------------|
| 2.4.7 Focus Visible (AA) | Focus indicator must be visible |
| 2.4.11 Focus Appearance (AAA) | Focus indicator must have 3:1 contrast, 2px thick |
| 2.4.12 Focus Not Obscured (AAA) | Focus indicator must not be hidden by other content |

**Focus Indicator Best Practices:**

1. **High contrast** - Use colors that stand out from the background
2. **Sufficient thickness** - At least 2 pixels wide
3. **Clear shape** - Outline the entire focusable area
4. **Consistent style** - Same indicator throughout your app
5. **Don't rely on color alone** - Use shape changes too

**Flutter's Default Focus:**

Flutter's Material widgets include focus indicators, but they're often subtle:
- `InkWell` shows a highlight
- `ElevatedButton` shows an overlay
- `TextField` changes border color

For custom widgets, you must implement focus indicators yourself.

