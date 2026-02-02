---
type: "THEORY"
title: "Cross Axis Alignment (Horizontal)"
---


Control how children are aligned horizontally:


**Options**:
- `CrossAxisAlignment.start` - Left edge
- `CrossAxisAlignment.center` - Centered (default)
- `CrossAxisAlignment.end` - Right edge
- `CrossAxisAlignment.stretch` - Fill width



```dart
Column(
  crossAxisAlignment: CrossAxisAlignment.start,  // Left-aligned
  children: [
    Text('Short'),
    Text('Medium text'),
    Text('Very long text here'),
  ],
)
```
