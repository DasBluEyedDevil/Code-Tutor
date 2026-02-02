---
type: "THEORY"
title: "Rounded Corners"
---


Use `BorderRadius.circular()` for uniform corners, or `BorderRadius.only()` to control each corner individually. Common use cases include:
- **Cards**: Slight rounding (8-15 pixels)
- **Buttons**: Pill shape (large radius or circular)
- **Tabs**: Top corners only




```dart
decoration: BoxDecoration(
  color: Colors.blue,
  borderRadius: BorderRadius.only(
    topLeft: Radius.circular(20),
    topRight: Radius.circular(20),
    bottomLeft: Radius.circular(0),
    bottomRight: Radius.circular(0),
  ),
)
```
