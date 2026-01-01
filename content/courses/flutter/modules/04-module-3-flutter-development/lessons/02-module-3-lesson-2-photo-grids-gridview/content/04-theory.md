---
type: "THEORY"
title: "Item Shape: childAspectRatio"
---

By default, GridView items are **squares**. To make them taller or wider, use `childAspectRatio`.

- **`1.0`**: Perfect square (default)
- **`2.0`**: Twice as wide as it is tall (Landscape)
- **`0.5`**: Twice as tall as it is wide (Portrait)

```dart
GridView.count(
  crossAxisCount: 2,
  childAspectRatio: 0.7, // Taller than wide (Portrait)
  children: [
    // Product cards often use this...
  ],
)
```