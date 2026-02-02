---
type: "THEORY"
title: "Container with a Child"
---


Containers can hold exactly ONE child widget. Use the `child` property to nest any widget inside. The container then wraps that widget with its styling (color, padding, etc.).

If you need multiple widgets, wrap them in a Column or Row first, then put that inside the Container.




```dart
Container(
  color: Colors.blue,
  padding: EdgeInsets.all(20),
  child: Text(
    'Hello!',
    style: TextStyle(color: Colors.white),
  ),
)
```
