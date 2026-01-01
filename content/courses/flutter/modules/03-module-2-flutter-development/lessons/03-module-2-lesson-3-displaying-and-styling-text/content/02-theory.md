---
type: "THEORY"
title: "The Basic Text Widget"
---


The simplest form:


This displays plain text in the center of the screen.



```dart
import 'package:flutter/material.dart';

void main() {
  runApp(
    MaterialApp(
      home: Scaffold(
        body: Center(
          child: Text('Hello, Flutter!'),
        ),
      ),
    ),
  );
}
```
