---
type: "THEORY"
title: "Padding - Space Inside"
---


**Conceptual**: Padding is like bubble wrap inside a box.




```dart
// Padding on all sides
Container(
  color: Colors.blue,
  padding: EdgeInsets.all(20),
  child: Text('Padded'),
)

// Different padding per side
Container(
  padding: EdgeInsets.only(
    left: 10,
    right: 10,
    top: 20,
    bottom: 20,
  ),
  child: Text('Custom Padding'),
)

// Symmetric padding
Container(
  padding: EdgeInsets.symmetric(
    horizontal: 20,  // left and right
    vertical: 10,    // top and bottom
  ),
  child: Text('Symmetric'),
)
```
