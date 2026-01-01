---
type: "THEORY"
title: "GridView.builder - Efficiency for Big Grids"
---

Just like `ListView.builder`, `GridView.builder` is much more efficient for large lists of data because it only builds what's visible on screen.

It requires a `gridDelegate` to define the layout:

```dart
GridView.builder(
  itemCount: 100,
  gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
    crossAxisCount: 3,
    mainAxisSpacing: 4,
    crossAxisSpacing: 4,
  ),
  itemBuilder: (context, index) {
    return Image.network('https://picsum.photos/200?random=$index');
  },
)
```
