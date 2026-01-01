---
type: "EXAMPLE"
title: "Your First Minimal Flutter App"
---


Let's create the simplest possible Flutter app. Create a new file called `minimal_app.dart`:


Let's run this! You should see a screen with "Hello, Flutter!" in the middle.



```dart
import 'package:flutter/material.dart';

void main() {
  runApp(
    MaterialApp(
      home: Center(
        child: Text('Hello, Flutter!'),
      ),
    ),
  );
}
```
