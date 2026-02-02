---
type: "THEORY"
title: "Multi-line Text"
---


### Line Breaks with \n


### Multi-line Strings


### Max Lines

Limit how many lines to show:




```dart
Text(
  'This is a very long text that might wrap to multiple lines',
  maxLines: 2,
  overflow: TextOverflow.ellipsis,  // Shows ... if text is cut off
)
```
