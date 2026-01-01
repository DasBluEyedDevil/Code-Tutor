---
type: "THEORY"
title: "Wrap - Auto-wrapping"
---


Like word wrap, but for widgets:


**Use case**: Tags, filter chips, buttons that wrap.



```dart
Wrap(
  spacing: 8,  // Horizontal spacing
  runSpacing: 8,  // Vertical spacing
  children: [
    Chip(label: Text('Flutter')),
    Chip(label: Text('Dart')),
    Chip(label: Text('Mobile')),
    Chip(label: Text('Development')),
    Chip(label: Text('UI')),
    // Auto-wraps to next line when needed!
  ],
)
```
