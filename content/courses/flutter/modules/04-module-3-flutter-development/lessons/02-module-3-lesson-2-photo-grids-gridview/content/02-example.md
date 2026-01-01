---
type: "EXAMPLE"
title: "Your First GridView"
---


`GridView.count` is the simplest way to create a grid. You specify `crossAxisCount` (number of columns), and GridView automatically arranges your children into rows.

This creates a 2-column grid with 4 colored boxes:



```dart
GridView.count(
  crossAxisCount: 2,  // 2 columns
  children: [
    Container(color: Colors.red),
    Container(color: Colors.blue),
    Container(color: Colors.green),
    Container(color: Colors.yellow),
  ],
)
```
