---
type: "THEORY"
title: "Horizontal Scrolling"
---



**Use case**: Image galleries, category chips.



```dart
SingleChildScrollView(
  scrollDirection: Axis.horizontal,
  child: Row(
    children: [
      Container(width: 200, color: Colors.red),
      Container(width: 200, color: Colors.blue),
      Container(width: 200, color: Colors.green),
    ],
  ),
)
```
