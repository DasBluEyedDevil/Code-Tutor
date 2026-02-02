---
type: "THEORY"
title: "InkWell - Make Anything Clickable"
---


`InkWell` wraps any widget and makes it respond to taps with a Material ripple effect. Use it when you need custom-looking buttons or tappable containers.

`GestureDetector` is similar but without the ripple. Choose `InkWell` for Material design, `GestureDetector` for custom effects:



```dart
InkWell(
  onTap: () {
    print('Container tapped!');
  },
  child: Container(
    padding: EdgeInsets.all(20),
    color: Colors.blue,
    child: Text('Tap me', style: TextStyle(color: Colors.white)),
  ),
)
```
