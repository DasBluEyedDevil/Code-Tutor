---
type: "THEORY"
title: "BoxDecoration - Advanced Styling"
---


For more complex styling, use `decoration`:


**Note**: When using `decoration`, put `color` inside `BoxDecoration`, not directly on Container!



```dart
Container(
  width: 200,
  height: 100,
  decoration: BoxDecoration(
    color: Colors.blue,
    borderRadius: BorderRadius.circular(20),  // Rounded corners
  ),
)
```
