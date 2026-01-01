---
type: "THEORY"
title: "Positioning Children with Positioned"
---

By default, all children of a `Stack` are aligned to the top-left. To move them around, wrap them in a `Positioned` widget.

`Positioned` lets you specify distance from the `top`, `bottom`, `left`, or `right` edges of the stack.

```dart
Stack(
  children: [
    Image.network('https://picsum.photos/300/200'),
    Positioned(
      bottom: 10,
      right: 10,
      child: Container(
        color: Colors.black54,
        padding: EdgeInsets.all(8),
        child: Text('Nature', style: TextStyle(color: Colors.white)),
      ),
    ),
  ],
)
```
