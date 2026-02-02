---
type: "THEORY"
title: "Alignment in Stack"
---

If you want to align all non-positioned children to a specific location (like the center), use the `alignment` property.

```dart
Stack(
  alignment: Alignment.center,
  children: [
    Icon(Icons.circle, size: 100, color: Colors.blue),
    Icon(Icons.star, size: 50, color: Colors.white),
  ],
)
```

Common alignment values:
- `Alignment.center`
- `Alignment.bottomRight`
- `Alignment.topCenter`
