---
type: "THEORY"
title: "The GestureDetector Widget"
---


Wrap ANY widget to make it detect gestures:




```dart
GestureDetector(
  onTap: () {
    print('Tapped!');
  },
  child: Container(
    width: 200,
    height: 200,
    color: Colors.blue,
    child: Center(child: Text('Tap Me')),
  ),
)
```
