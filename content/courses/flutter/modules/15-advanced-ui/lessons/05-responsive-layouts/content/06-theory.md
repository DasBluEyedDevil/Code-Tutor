---
type: "THEORY"
title: "Breakpoint Patterns - Defining Responsive Breakpoints"
---


**Breakpoints** are width thresholds where your layout changes. Define them once and reuse throughout your app.

**Common Breakpoint Systems:**

| System | Phone | Tablet | Desktop |
|--------|-------|--------|--------|
| Material | <600 | 600-1024 | >1024 |
| Bootstrap | <576 | 576-992 | >992 |
| Tailwind | <640 | 640-1024 | >1024 |

**Creating a Breakpoint System:**

```dart
enum ScreenSize { compact, medium, expanded }

class Breakpoints {
  static const double compact = 600;
  static const double medium = 840;
  static const double expanded = 1200;
  
  static ScreenSize getScreenSize(double width) {
    if (width < compact) return ScreenSize.compact;
    if (width < medium) return ScreenSize.medium;
    return ScreenSize.expanded;
  }
}
```

**Material 3 Window Size Classes:**

Google recommends these breakpoints for Material apps:

| Class | Width | Typical Device |
|-------|-------|----------------|
| Compact | <600dp | Phone |
| Medium | 600-840dp | Tablet portrait, foldable |
| Expanded | >840dp | Tablet landscape, desktop |

