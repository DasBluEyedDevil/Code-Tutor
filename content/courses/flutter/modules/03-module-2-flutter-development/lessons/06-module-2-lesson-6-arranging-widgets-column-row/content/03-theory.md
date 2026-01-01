---
type: "THEORY"
title: "Main Axis Alignment (Vertical)"
---


Control how children are spaced vertically:


**Options**:
- `MainAxisAlignment.start` - At the top
- `MainAxisAlignment.center` - Centered vertically
- `MainAxisAlignment.end` - At the bottom
- `MainAxisAlignment.spaceBetween` - Space between items
- `MainAxisAlignment.spaceAround` - Space around items
- `MainAxisAlignment.spaceEvenly` - Equal spacing



```dart
Column(
  mainAxisAlignment: MainAxisAlignment.start,  // Default: top
  children: [
    Text('Item 1'),
    Text('Item 2'),
    Text('Item 3'),
  ],
)
```
