---
type: "THEORY"
title: "The Stack Widget"
---

The `Stack` widget allows you to place multiple children on top of each other. The order of children determines the layering: the **first** child is at the bottom, and the **last** child is at the top.

```dart
Stack(
  children: [
    Container(width: 200, height: 200, color: Colors.blue),
    Container(width: 150, height: 150, color: Colors.red),
    Container(width: 100, height: 100, color: Colors.green),
  ],
)
```

In this example, the green box is on top, and the blue box is at the bottom.