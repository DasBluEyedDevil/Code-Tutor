---
type: "THEORY"
title: "Gesture Priority"
---


**Problem**: What if you have overlapping gestures?


**Result**: Only "Child tap" prints (child wins)

To allow parent to handle: Use `behavior: HitTestBehavior.translucent`



```dart
GestureDetector(
  onTap: () => print('Parent tap'),
  child: Container(
    color: Colors.blue,
    padding: EdgeInsets.all(50),
    child: GestureDetector(
      onTap: () => print('Child tap'),
      child: Container(
        color: Colors.red,
        width: 100,
        height: 100,
      ),
    ),
  ),
)
```
