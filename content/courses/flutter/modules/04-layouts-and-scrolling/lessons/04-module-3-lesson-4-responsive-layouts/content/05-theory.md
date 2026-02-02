---
type: "THEORY"
title: "OrientationBuilder - Portrait vs Landscape"
---

Sometimes you just want to know if the phone is being held vertically or horizontally.

```dart
OrientationBuilder(
  builder: (context, orientation) {
    return orientation == Orientation.portrait
        ? Column(children: [ ... ])
        : Row(children: [ ... ]);
  },
)
```