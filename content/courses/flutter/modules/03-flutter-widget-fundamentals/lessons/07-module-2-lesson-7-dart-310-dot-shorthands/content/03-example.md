---
type: "EXAMPLE"
title: "Modern Dart 3.10+ Syntax"
---


With Dart 3.10+, when the type is known from context, you can use the shorthand dot syntax. The code becomes much cleaner:



```dart
Column(
  mainAxisAlignment: .center,
  crossAxisAlignment: .start,
  children: [
    Text(
      'Hello',
      textAlign: .center,
      style: TextStyle(fontWeight: .bold),
    ),
    Image.asset('photo.png', fit: .cover),
  ],
)
```
