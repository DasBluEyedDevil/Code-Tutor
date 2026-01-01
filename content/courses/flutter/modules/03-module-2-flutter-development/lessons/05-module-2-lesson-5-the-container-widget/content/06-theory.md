---
type: "THEORY"
title: "Width and Height"
---


Containers have no size by default - they expand to fit their child, or shrink to nothing if empty. Set explicit `width` and `height` to control the size.

**Special value:** Use `double.infinity` to take up all available space in that dimension. This is useful for full-width buttons or dividers.




```dart
// Take up all available width
Container(
  width: double.infinity,
  height: 100,
  color: Colors.orange,
)

// Take up all available height
Container(
  width: 100,
  height: double.infinity,
  color: Colors.purple,
)
```
