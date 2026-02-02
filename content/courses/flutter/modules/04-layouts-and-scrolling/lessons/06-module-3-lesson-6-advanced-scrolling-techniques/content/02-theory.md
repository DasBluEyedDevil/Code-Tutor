---
type: "THEORY"
title: "SingleChildScrollView"
---


Makes ANY widget scrollable:


**Use case**: Forms, long content that might overflow.



```dart
SingleChildScrollView(
  child: Column(
    children: [
      Container(height: 200, color: Colors.red),
      Container(height: 200, color: Colors.blue),
      Container(height: 200, color: Colors.green),
      Container(height: 200, color: Colors.yellow),
      // If total height > screen, it scrolls!
    ],
  ),
)
```
