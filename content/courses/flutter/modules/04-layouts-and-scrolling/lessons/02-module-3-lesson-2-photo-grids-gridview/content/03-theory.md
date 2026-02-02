---
type: "THEORY"
title: "Controlling Grid Spacing"
---

A grid without spacing looks crowded. Use these properties to add breathing room:

- **`crossAxisSpacing`**: Space between columns
- **`mainAxisSpacing`**: Space between rows
- **`padding`**: Space around the entire grid

```dart
GridView.count(
  crossAxisCount: 3,
  crossAxisSpacing: 10,  // Horizontal gap
  mainAxisSpacing: 10,   // Vertical gap
  padding: EdgeInsets.all(10),
  children: [
    // ...
  ],
)
```