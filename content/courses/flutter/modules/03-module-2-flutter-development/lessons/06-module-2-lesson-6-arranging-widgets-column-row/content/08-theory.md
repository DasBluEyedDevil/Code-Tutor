---
type: "THEORY"
title: "Expanded - Taking Up Available Space"
---


Sometimes you want a child to take up all remaining space:




```dart
Row(
  children: [
    Icon(Icons.menu),
    Expanded(
      child: Text('This takes up remaining space'),
    ),
    Icon(Icons.search),
  ],
)
```
