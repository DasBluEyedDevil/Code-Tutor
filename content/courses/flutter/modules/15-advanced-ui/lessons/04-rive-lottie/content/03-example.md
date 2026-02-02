---
type: "EXAMPLE"
title: "Basic Lottie Usage"
---


Load and display Lottie animations:



```dart
import 'package:flutter/material.dart';
import 'package:lottie/lottie.dart';

class LottieExamples extends StatelessWidget {
  const LottieExamples({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // Load from assets (add to pubspec.yaml assets first)
        Lottie.asset(
          'assets/animations/loading.json',
          width: 200,
          height: 200,
          fit: BoxFit.contain,
        ),
        
        // Load from network
        Lottie.network(
          'https://assets.lottiefiles.com/packages/lf20_jcikwtux.json',
          width: 150,
          height: 150,
          repeat: true,
        ),
        
        // Play once, no loop
        Lottie.asset(
          'assets/animations/success.json',
          repeat: false,
        ),
        
        // Reverse after playing
        Lottie.asset(
          'assets/animations/toggle.json',
          reverse: true,
          repeat: true,
        ),
      ],
    );
  }
}

// Add to pubspec.yaml:
// flutter:
//   assets:
//     - assets/animations/
```
