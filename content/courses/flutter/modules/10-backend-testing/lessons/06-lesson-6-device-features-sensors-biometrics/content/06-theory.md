---
type: "THEORY"
title: "Part 4: Vibration & Haptic Feedback"
---


### Setup

**pubspec.yaml:**

**Android Configuration (`android/app/src/main/AndroidManifest.xml`):**

### Vibration Examples


### Haptic Feedback (Alternative)

Flutter has built-in haptic feedback:


**Example in Button:**



```dart
ElevatedButton(
  onPressed: () {
    HapticFeedback.lightImpact();  // Provide feedback
    // ... do action
  },
  child: Text('Tap Me'),
)
```
