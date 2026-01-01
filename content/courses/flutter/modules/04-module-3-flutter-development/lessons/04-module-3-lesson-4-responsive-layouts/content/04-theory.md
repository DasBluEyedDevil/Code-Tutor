---
type: "THEORY"
title: "LayoutBuilder - Parent-Based Constraints"
---

While `MediaQuery` tells you about the **screen**, `LayoutBuilder` tells you about the **parent widget's size**. This is often more useful for building reusable components.

```dart
LayoutBuilder(
  builder: (context, constraints) {
    if (constraints.maxWidth > 600) {
      return Row(children: [ ... ]); // Wide layout
    } else {
      return Column(children: [ ... ]); // Narrow layout
    }
  },
)
```