---
type: "THEORY"
title: "Margin - Space Outside"
---


**Conceptual**: Margin is like the space between boxes on a shelf.


**Margin vs Padding**:
- **Padding**: Space between container edge and its child (inside)
- **Margin**: Space between container and other widgets (outside)



```dart
Container(
  margin: EdgeInsets.all(20),
  color: Colors.red,
  child: Text('Has margin'),
)
```
