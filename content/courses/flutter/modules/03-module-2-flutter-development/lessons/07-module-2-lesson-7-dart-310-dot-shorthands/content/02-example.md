---
type: "EXAMPLE"
title: "Before vs After - Traditional Verbose Syntax"
---


Here's what Flutter code looked like before Dart 3.10. Notice how we have to repeat the enum name every time:



```dart
Column(
  mainAxisAlignment: MainAxisAlignment.center,
  crossAxisAlignment: CrossAxisAlignment.start,
  children: [
    Text(
      'Hello',
      textAlign: TextAlign.center,
      style: TextStyle(fontWeight: FontWeight.bold),
    ),
    Image.asset('photo.png', fit: BoxFit.cover),
  ],
)
```
