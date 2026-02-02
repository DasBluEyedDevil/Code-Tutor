---
type: "THEORY"
title: "Loading Indicator"
---


Show a loading spinner while image loads:




```dart
Image.network(
  'https://picsum.photos/200/300',
  loadingBuilder: (context, child, progress) {
    if (progress == null) return child;
    return CircularProgressIndicator();
  },
)
```
