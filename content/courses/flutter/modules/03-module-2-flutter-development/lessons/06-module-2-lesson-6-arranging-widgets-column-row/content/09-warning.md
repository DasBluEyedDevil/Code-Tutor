---
type: "WARNING"
title: "Common Mistakes"
---


### 1. Column without Constrained Height


### 2. Row/Column Overflow




```dart
// If children are too wide/tall, wrap in SingleChildScrollView:
SingleChildScrollView(
  child: Column(
    children: [
      // Many children...
    ],
  ),
)
```
