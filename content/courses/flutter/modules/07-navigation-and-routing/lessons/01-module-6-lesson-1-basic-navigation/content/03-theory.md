---
type: "THEORY"
title: "Understanding Navigator.push"
---



**MaterialPageRoute** creates a platform-specific transition:
- **iOS**: Slide from right
- **Android**: Slide up



```dart
Navigator.push(
  context,                                      // Where we are
  MaterialPageRoute(builder: (context) => DetailScreen()),  // Where we're going
);
```
