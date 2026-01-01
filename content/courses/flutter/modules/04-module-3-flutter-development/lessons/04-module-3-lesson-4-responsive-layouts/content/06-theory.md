---
type: "THEORY"
title: "Wrap - The Dynamic Row"
---

A `Row` crashes if its children are too wide. A `Wrap` widget automatically moves children to the next line (like text in a word processor).

```dart
Wrap(
  spacing: 8.0, // gap between adjacent chips
  runSpacing: 4.0, // gap between lines
  children: [
    Chip(label: Text('Flutter')),
    Chip(label: Text('Dart')),
    Chip(label: Text('Responsive')),
    Chip(label: Text('Design')),
  ],
)
```
