---
type: "THEORY"
title: "Tap vs InkWell"
---


**GestureDetector**: No visual feedback
**InkWell**: Material ripple effect


**Best Practice**: Use InkWell for Material Design apps!



```dart
// No visual feedback
GestureDetector(
  onTap: () => print('Tap'),
  child: Container(
    color: Colors.blue,
    padding: EdgeInsets.all(20),
    child: Text('Tap Me'),
  ),
)

// With ripple effect
InkWell(
  onTap: () => print('Tap'),
  child: Container(
    padding: EdgeInsets.all(20),
    child: Text('Tap Me'),
  ),
)
```
