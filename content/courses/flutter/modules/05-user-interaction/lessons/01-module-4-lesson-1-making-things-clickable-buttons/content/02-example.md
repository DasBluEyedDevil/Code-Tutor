---
type: "EXAMPLE"
title: "Your First Button"
---



Run this and click the button - check the console!



```dart
import 'package:flutter/material.dart';

void main() {
  runApp(
    MaterialApp(
      home: Scaffold(
        body: Center(
          child: ElevatedButton(
            onPressed: () {
              print('Button pressed!');
            },
            child: Text('Click Me'),
          ),
        ),
      ),
    ),
  );
}
```
