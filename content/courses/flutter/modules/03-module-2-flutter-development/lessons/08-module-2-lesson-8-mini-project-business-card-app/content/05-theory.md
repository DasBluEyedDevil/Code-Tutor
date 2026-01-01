---
type: "THEORY"
title: "Step 2: Basic Structure"
---


Replace everything in `main.dart`:


Run it! You should see a teal screen with text.



```dart
import 'package:flutter/material.dart';

void main() {
  runApp(BusinessCardApp());
}

class BusinessCardApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Business Card',
      home: BusinessCardScreen(),
    );
  }
}

class BusinessCardScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.teal,
      body: SafeArea(
        child: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Text('Your card will go here'),
            ],
          ),
        ),
      ),
    );
  }
}
```
