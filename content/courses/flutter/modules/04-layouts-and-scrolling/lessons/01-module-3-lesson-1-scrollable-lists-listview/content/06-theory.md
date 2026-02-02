---
type: "THEORY"
title: "Horizontal ListView"
---


Lists can scroll horizontally too:




```dart
ListView(
  scrollDirection: Axis.horizontal,
  children: [
    Container(width: 160, color: Colors.red),
    Container(width: 160, color: Colors.blue),
    Container(width: 160, color: Colors.green),
  ],
)
```
