---
type: "THEORY"
title: "Circular Images"
---


Use `CircleAvatar`:


Or use `ClipOval`:




```dart
ClipOval(
  child: Image.network(
    'https://picsum.photos/200',
    width: 100,
    height: 100,
    fit: BoxFit.cover,
  ),
)
```
