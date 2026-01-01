---
type: "THEORY"
title: "Haptic Feedback"
---


Add tactile feedback for better UX:




```dart
import 'package:flutter/services.dart';

GestureDetector(
  onTap: () {
    HapticFeedback.lightImpact();  // Subtle vibration
    print('Tapped!');
  },
  onLongPress: () {
    HapticFeedback.heavyImpact();  // Stronger vibration
    print('Long pressed!');
  },
  child: YourWidget(),
)
```
