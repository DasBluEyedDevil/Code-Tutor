---
type: "THEORY"
title: "Combining Column and Row"
---


This is where it gets powerful!




```dart
Column(
  children: [
    Text('Header'),
    Row(
      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
      children: [
        Icon(Icons.favorite),
        Icon(Icons.star),
        Icon(Icons.share),
      ],
    ),
    Text('Footer'),
  ],
)
```
