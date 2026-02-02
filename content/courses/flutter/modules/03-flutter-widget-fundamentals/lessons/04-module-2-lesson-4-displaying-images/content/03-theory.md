---
type: "THEORY"
title: "Network Images - The Easy Way"
---


Display an image from a URL:


**That's it!** The image loads from the internet and displays.



```dart
import 'package:flutter/material.dart';

void main() {
  runApp(
    MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: Text('Network Image')),
        body: Center(
          child: Image.network(
            'https://picsum.photos/200/300',
          ),
        ),
      ),
    ),
  );
}
```
