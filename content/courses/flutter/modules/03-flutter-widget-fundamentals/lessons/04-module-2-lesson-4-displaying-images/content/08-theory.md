---
type: "THEORY"
title: "Error Handling"
---


What if the image fails to load?




```dart
Image.network(
  'https://invalid-url.com/image.jpg',
  errorBuilder: (context, error, stackTrace) {
    return Text('Failed to load image');
  },
)
```
